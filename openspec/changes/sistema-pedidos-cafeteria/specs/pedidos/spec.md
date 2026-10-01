# Pedidos

## ADDED Requirements

### Requirement: Estados del pedido
El pedido MUST manejar los estados `CREADO`, `PERSONALIZADO`, `PAGO_PROCESADO`, `CONFIRMADO` y
`RECHAZADO`, solo en memoria. El camino exitoso es
`CREADO → PERSONALIZADO → PAGO_PROCESADO → CONFIRMADO`. Cualquier fallo de validación o de pago
MUST llevar el pedido a `RECHAZADO`. `PAGO_PROCESADO` solo se alcanza cuando el pago fue aprobado.

#### Scenario: Camino exitoso
- **WHEN** un pedido válido se paga con un medio de pago que aprueba
- **THEN** el pedido recorre `CREADO`, `PERSONALIZADO`, `PAGO_PROCESADO` y termina en `CONFIRMADO`

#### Scenario: Camino con fallo
- **WHEN** ocurre cualquier fallo de validación o el pago es rechazado
- **THEN** el pedido termina en `RECHAZADO` y nunca en `CONFIRMADO`

### Requirement: Facade coordina el proceso completo
`PedidoFacade` MUST ofrecer una operación única que reciba el producto, los complementos y el
medio de pago (por nombre) y devuelva el `Pedido` resultante. Internamente MUST coordinar Factory
Method, Decorator, Adapter y la validación en ese orden, sin que el cliente conozca sus detalles.

#### Scenario: Cliente usa solo el Facade
- **WHEN** el cliente pide `Cafe` con `Leche` pagado con `Nequi`
- **THEN** obtiene un `Pedido` con estado, descripción, total y mensaje
- **AND** el cliente no referencia ninguna clase de creador, decorador ni adaptador

#### Scenario: El Facade no contiene la lógica de los componentes
- **WHEN** se revisa `PedidoFacade`
- **THEN** delega la creación en un `ProductoCreator`, el cálculo de precio en el `Producto`
  decorado, el pago en un `IPayment` y las validaciones en `ValidadorPedido`

### Requirement: Validación previa al pago
Antes de procesar el pago, el sistema MUST validar que exista un producto (RF-09) y que exista un
medio de pago (RF-10). Si alguna validación falla, el pedido MUST quedar `RECHAZADO` y el pago
MUST NOT procesarse.

#### Scenario: Sin producto
- **WHEN** el producto solicitado no existe
- **THEN** el pedido queda `RECHAZADO` y no se invoca ningún pago

#### Scenario: Sin medio de pago
- **WHEN** el medio de pago solicitado no existe
- **THEN** el pedido queda `RECHAZADO` con un mensaje que lo indica y no se procesa ningún pago

### Requirement: Validación del resultado del pago
Después de procesar el pago, el sistema MUST validar su resultado (RF-11).

#### Scenario: Pago aprobado
- **WHEN** el resultado del pago indica aprobación
- **THEN** la validación es correcta y el pedido pasa a `PAGO_PROCESADO`

#### Scenario: Pago rechazado
- **WHEN** el resultado del pago indica rechazo
- **THEN** la validación falla y el pedido pasa a `RECHAZADO`

### Requirement: Confirmación solo con pago aprobado
El pedido MUST confirmarse únicamente cuando el pago haya sido aprobado y todas las validaciones
sean correctas (RF-12). El propio `Pedido` MUST impedir confirmarse si su estado no es
`PAGO_PROCESADO`.

#### Scenario: Pago aprobado y validaciones correctas
- **WHEN** el pago es aprobado y las validaciones son correctas
- **THEN** el pedido queda `CONFIRMADO`

#### Scenario: Pago rechazado
- **WHEN** el pago es rechazado
- **THEN** el pedido no queda confirmado; su estado es `RECHAZADO`

#### Scenario: Confirmación fuera de orden
- **WHEN** se intenta confirmar un pedido cuyo estado no es `PAGO_PROCESADO`
- **THEN** la confirmación no se realiza y el estado no cambia a `CONFIRMADO`

### Requirement: Resultado final visible
El sistema MUST mostrar al usuario el resultado del pedido (RF-14): estado, descripción del
producto con sus complementos, total y mensaje.

#### Scenario: El pedido ofrece un resumen para mostrar
- **WHEN** se solicita el resumen de un pedido (`Pedido.ObtenerResumen`)
- **THEN** el texto incluye el estado, la descripción del producto con sus complementos, el total y
  el mensaje
- **AND** `Program` imprime ese resumen sin componer el texto por su cuenta

#### Scenario: Ejemplo de punta a punta
- **WHEN** el usuario pide un Café con Leche y paga con Nequi con fondos suficientes
- **THEN** el resultado muestra estado `CONFIRMADO`, el producto con Leche y un total de 4000
