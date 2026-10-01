# Productos

## ADDED Requirements

### Requirement: Catálogo de productos
El sistema MUST ofrecer cuatro productos, cada uno con nombre, descripción y precio base. Los
precios son valores de prueba en COP, representados con `decimal`:

| Producto | Precio base |
|---|---|
| Café | 3000 |
| Capuchino | 4500 |
| Latte | 5000 |
| Chocolate | 4000 |

#### Scenario: Crear Café
- **WHEN** el usuario selecciona `Cafe` y se solicita crear el producto
- **THEN** se obtiene una instancia válida de Café con nombre, descripción y precio base 3000

#### Scenario: Crear los demás productos
- **WHEN** el usuario selecciona `Capuchino`, `Latte` o `Chocolate`
- **THEN** se obtiene una instancia válida del producto correspondiente con su precio base
  (4500, 5000 y 4000 respectivamente)

### Requirement: Creación mediante Factory Method
La creación de productos MUST realizarse con Factory Method: existe una abstracción común
`Producto`, una clase abstracta `ProductoCreator` con la operación de creación, y un creador
concreto por producto (`CafeCreator`, `CapuchinoCreator`, `LatteCreator`, `ChocolateCreator`).
El código cliente MUST trabajar únicamente con la abstracción `Producto` y no MUST instanciar
clases concretas de producto.

#### Scenario: El creador concreto devuelve su producto
- **WHEN** se invoca la operación de creación de `CafeCreator`
- **THEN** el resultado es un `Producto` cuyo nombre es Café
- **AND** lo mismo aplica a cada creador concreto con su producto correspondiente

#### Scenario: El cliente depende solo de abstracciones
- **WHEN** se revisa el código de `PedidoFacade`
- **THEN** solo referencia `Producto` y `ProductoCreator`, nunca `Cafe`, `Capuchino`, `Latte`
  ni `Chocolate`

### Requirement: Rechazo controlado de producto no soportado
Si se solicita un producto que no existe en el catálogo, el sistema MUST rechazar la operación de
forma controlada: sin lanzar una excepción no manejada, sin realizar ningún cobro y informando el
motivo.

#### Scenario: Producto inválido
- **WHEN** el usuario solicita un producto no soportado, por ejemplo `Espresso`
- **THEN** el pedido queda en estado `RECHAZADO` con un mensaje que indica que el producto no es
  soportado
- **AND** no se procesa ningún pago
