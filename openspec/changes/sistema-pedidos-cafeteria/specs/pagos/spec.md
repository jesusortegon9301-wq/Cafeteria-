# Pagos

## ADDED Requirements

### Requirement: Interfaz común de pago
El sistema MUST definir una interfaz común `IPayment` con el nombre del medio de pago y una
operación `Pagar(monto)` que devuelve un resultado con el estado de aprobación y un mensaje.
El código principal MUST usar únicamente `IPayment` y no MUST depender de las diferencias entre
medios de pago.

#### Scenario: Uso uniforme de cualquier medio de pago
- **WHEN** el código principal recibe un `IPayment`
- **THEN** puede procesar el pago con `Pagar(monto)` sin saber si es Nequi, Daviplata o Tarjeta

### Requirement: Medios de pago mediante Adapter
Los medios de pago MUST implementarse con Adapter. Existen tres adaptadores
(`NequiAdapter`, `DaviplataAdapter`, `TarjetaAdapter`), cada uno envolviendo una API simulada
con firma y tipo de resultado distintos:

| Adaptado (API simulada) | Operación | Resultado |
|---|---|---|
| `NequiApi` | `EnviarPago(celular, valor)` | `bool` |
| `DaviplataApi` | `Transferir(cuenta, monto)` | `string` con código (`"00"` = aprobado) |
| `TarjetaApi` | `Cobrar(numero, monto)` | `int` (0 = denegado, otro valor = autorización) |

#### Scenario: Pago con Nequi
- **WHEN** se paga un monto dentro de los fondos disponibles usando `NequiAdapter`
- **THEN** el resultado es aprobado

#### Scenario: Pago con Daviplata
- **WHEN** se paga un monto dentro de los fondos disponibles usando `DaviplataAdapter`
- **THEN** el resultado es aprobado

#### Scenario: Pago con Tarjeta
- **WHEN** se paga un monto dentro de los fondos disponibles usando `TarjetaAdapter`
- **THEN** el resultado es aprobado

### Requirement: Pagos simulados y deterministas
Los pagos MUST ser simulados: no MUST haber llamadas de red, integraciones reales ni transacciones
reales. El resultado MUST depender solo de los datos configurados (fondos disponibles frente al
monto), de modo que el mismo monto produzca siempre el mismo resultado.

#### Scenario: Fondos insuficientes
- **WHEN** se paga un monto mayor que los fondos disponibles del medio de pago
- **THEN** el resultado es rechazado con un mensaje explicativo

#### Scenario: Resultado repetible
- **WHEN** se procesa dos veces el mismo monto con el mismo medio de pago y los mismos fondos
- **THEN** ambos resultados son idénticos

### Requirement: Selección del medio de pago
El usuario MUST poder seleccionar uno de los tres medios de pago por nombre (`Nequi`,
`Daviplata`, `Tarjeta`), sin distinguir mayúsculas de minúsculas.

#### Scenario: Medio de pago válido
- **WHEN** el usuario selecciona `nequi`
- **THEN** el sistema usa el adaptador de Nequi

#### Scenario: Medio de pago inexistente
- **WHEN** el usuario selecciona un medio no soportado, por ejemplo `Bitcoin`
- **THEN** no se obtiene ningún medio de pago y el pedido es rechazado (ver capacidad `pedidos`)
