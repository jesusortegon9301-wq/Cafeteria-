# Personalización

## ADDED Requirements

### Requirement: Catálogo de complementos
El sistema MUST ofrecer cuatro complementos, cada uno con un precio adicional (valores de prueba
en COP, `decimal`):

| Complemento | Precio |
|---|---|
| Leche | 1000 |
| Crema | 1500 |
| Caramelo | 1200 |
| Azúcar | 500 |

#### Scenario: Complementos disponibles
- **WHEN** se consulta el catálogo de complementos
- **THEN** existen Leche, Crema, Caramelo y Azúcar con los precios indicados

### Requirement: Complementos mediante Decorator
Los complementos MUST implementarse con Decorator: cada complemento es a la vez un `Producto` y
envuelve a otro `Producto`, sumando su precio y añadiendo su nombre a la descripción. El sistema
MUST NOT definir clases por combinación de producto y complementos.

#### Scenario: Un complemento es un Producto
- **WHEN** se envuelve un `Producto` con `LecheDecorator`
- **THEN** el resultado puede usarse en cualquier lugar donde se espere un `Producto`

#### Scenario: Combinación dinámica en tiempo de ejecución
- **WHEN** se envuelve un Café con Leche, luego con Crema y luego con Caramelo
- **THEN** el resultado es un único `Producto` que acumula los tres complementos
- **AND** no existe ninguna clase concreta que represente esa combinación

### Requirement: Cálculo del precio final
El precio final MUST calcularse como `precio base + suma de los precios de los complementos`.
Se permiten cero, uno o varios complementos.

#### Scenario: Producto sin complementos
- **WHEN** el pedido es un Café sin complementos
- **THEN** el precio final es 3000, igual al precio base

#### Scenario: Producto con un complemento
- **WHEN** se agrega Leche a un Café
- **THEN** el precio final es 4000

#### Scenario: Producto con varios complementos
- **WHEN** se agregan Leche, Crema y Caramelo a un Café
- **THEN** el precio final es 6700 (3000 + 1000 + 1500 + 1200)

#### Scenario: La descripción refleja los complementos
- **WHEN** se agregan Leche y Crema a un Café
- **THEN** la descripción del producto resultante incluye el nombre de ambos complementos

### Requirement: Rechazo de complemento no soportado
Si se solicita un complemento que no existe en el catálogo, el sistema MUST rechazar el pedido de
forma controlada, sin cobrar y sin ignorar el complemento en silencio.

#### Scenario: Complemento inválido
- **WHEN** el usuario solicita un complemento no soportado, por ejemplo `Canela`
- **THEN** el pedido queda en estado `RECHAZADO` con un mensaje que indica el complemento no
  soportado
- **AND** no se procesa ningún pago
