# Tareas

Cada tarea es pequeña y verificable. Se implementan en orden con `/opsx:apply`. No se agrega
ninguna funcionalidad fuera de estas tareas sin modificar antes la especificación.

## 1. Estructura del proyecto

- [x] 1.1 Mover `Cafeteria/` a `src/Cafeteria/` y actualizar la ruta en `Cafeteria.sln`
- [x] 1.2 Crear `tests/Cafeteria.Tests` (xUnit, net8.0) con referencia a `src/Cafeteria`
- [x] 1.3 Agregar el proyecto de pruebas al `.sln`
- [ ] 1.4 Verificar que `dotnet build` y `dotnet test` funcionan (sin pruebas aún) — pendiente: requiere el SDK de .NET (no disponible en el entorno de implementación)

## 2. Abstracción Producto

- [x] 2.1 Crear la clase abstracta `Producto` (Nombre, Descripcion, Precio)

## 3. Factory Method

- [x] 3.1 Crear la clase abstracta `ProductoCreator` con `CrearProducto()`
- [x] 3.2 Crear `CafeCreator`, `CapuchinoCreator`, `LatteCreator` y `ChocolateCreator`

## 4. Productos concretos

- [x] 4.1 Crear `Cafe` (3000), `Capuchino` (4500), `Latte` (5000) y `Chocolate` (4000)
- [ ] 4.2 Verificar que cada creador devuelve su producto (CA-01, CA-02) — pruebas escritas; pendiente ejecutarlas

## 5. Decorator

- [x] 5.1 Crear `ProductoDecorator` (es un `Producto` y envuelve otro `Producto`)

## 6. Complementos

- [x] 6.1 Crear `LecheDecorator` (1000), `CremaDecorator` (1500), `CarameloDecorator` (1200) y
      `AzucarDecorator` (500)
- [ ] 6.2 Verificar precio y descripción acumulados con uno y con varios complementos — pruebas escritas; pendiente ejecutarlas
      (CA-04, CA-05, CA-06)

## 7. Interfaz de pago

- [x] 7.1 Crear `ResultadoPago` (record) e `IPayment`

## 8. Adapter

- [x] 8.1 Crear las APIs simuladas `NequiApi`, `DaviplataApi` y `TarjetaApi` con firmas
      incompatibles y fondos configurables
- [x] 8.2 Crear `NequiAdapter`, `DaviplataAdapter` y `TarjetaAdapter` implementando `IPayment`
- [ ] 8.3 Verificar aprobación y rechazo por fondos en los tres medios (CA-07, CA-08) — pruebas escritas; pendiente ejecutarlas

## 9. Pedido

- [x] 9.1 Crear `EstadoPedido`
- [x] 9.2 Crear `Pedido` con sus transiciones, impidiendo confirmar fuera de `PAGO_PROCESADO`

## 10. Facade

- [x] 10.1 Crear `PedidoFacade` con los diccionarios de creadores, complementos y medios de pago
- [x] 10.2 Agregar el constructor que recibe medios de pago inyectados (para pruebas)
- [x] 10.3 Implementar `RealizarPedido` siguiendo el orden definido en `design.md`
- [x] 10.4 Rechazar producto, complemento o medio de pago desconocido sin lanzar excepciones

## 11. Validaciones

- [x] 11.1 Crear `ValidadorPedido` con `ValidarAntesDePago` y `ValidarPago`
- [x] 11.2 Conectar el validador al Facade en los dos momentos (antes y después del pago)

## 12. Programa de consola

- [x] 12.1 Crear `Program.cs` con un menú numerado (producto, complementos, medio de pago)
- [x] 12.2 Mostrar el resultado: estado, descripción, total y mensaje
- [x] 12.3 Verificar que `Program` solo usa `PedidoFacade`
- [x] 12.4 Agregar `Pedido.ObtenerResumen` (RF-14) y usarlo en `Program`

## 13. Pruebas

- [x] 13.1 Creación de los cuatro productos y rechazo de producto inválido (CA-01, CA-02, CA-03)
- [x] 13.2 Sin complementos, con uno y con varios; cálculo del precio (CA-04, CA-05, CA-06)
- [x] 13.3 Pago con Nequi, Daviplata y Tarjeta (CA-07)
- [x] 13.4 Pago rechazado: el pedido no queda confirmado (CA-08)
- [x] 13.5 Confirmación de pedido con pago aprobado (CA-09)
- [x] 13.6 Proceso completo usando solo `PedidoFacade` (CA-10)
- [x] 13.7 Complemento y medio de pago desconocidos rechazan el pedido
- [x] 13.8 Nombrar cada prueba con su criterio (por ejemplo `CA05_...`) para facilitar la verificación
- [x] 13.9 Probar el resumen del pedido (RF-14)

## 14. README

- [x] 14.1 Crear `README.md` con: nombre, problema, objetivo, usuario, los cuatro patrones, flujo,
      estructura, requisitos, instrucciones de ejecución, ejemplo y resultado esperado

## 15. Verificación

- [ ] 15.1 Ejecutar `dotnet build` y `dotnet test`; todas las pruebas pasan
- [ ] 15.2 Ejecutar la aplicación y comprobar el ejemplo de punta a punta (Café + Leche + Nequi
      → `CONFIRMADO`, total 4000)
- [x] 15.3 Revisar que todo el código está en `src/` y no hay funcionalidades fuera de alcance
- [x] 15.4 Ejecutar `/opsx:verify` — verificación estática en `docs/04-verificacion.md`; falta ejecutar `dotnet build` y `dotnet test`
