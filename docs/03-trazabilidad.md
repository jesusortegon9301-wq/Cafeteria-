# Trazabilidad: de la especificación base al código y las pruebas

Relaciona cada requisito (RF) y criterio de aceptación (CA) de
[`02-especificacion-base.md`](02-especificacion-base.md) con la spec de OpenSpec que lo describe,
el código que lo implementa y la prueba que lo demuestra.

Las pruebas están en `tests/Cafeteria.Tests/`. Los nombres de los métodos aparecen tal como están
en el código.

## Requisitos funcionales

| RF | Requisito | Spec (`openspec/changes/sistema-pedidos-cafeteria/specs/`) | Código | Prueba |
|---|---|---|---|---|
| RF-01 | Seleccionar un producto | `productos` — Catálogo de productos | `PedidoFacade.RealizarPedido` | `CA01_…`, `CA02_…`, `CA10_…` |
| RF-02 | Crear con Factory Method | `productos` — Creación mediante Factory Method | `Creadores/` | `CA01_CrearCafe_…`, `CA02_CrearOtrosProductos_…` |
| RF-03 | Cero o varios complementos | `personalizacion` — Cálculo del precio final | `PedidoFacade`, `Complementos/` | `CA04_…`, `CA05_…`, `CA06_…` |
| RF-04 | Complementos con Decorator | `personalizacion` — Complementos mediante Decorator | `Complementos/ProductoDecorator.cs` | `Decorator_ElComplementoEsUnProducto_…`, `Decorator_LaDescripcionReflejaLosComplementos` |
| RF-05 | Calcular el precio final | `personalizacion` — Cálculo del precio final | `ProductoDecorator.Precio` | `CA06_VariosComplementos_SumaTodos`, `Facade_AplicaLosComplementosPedidos_YCalculaElTotal` |
| RF-06 | Seleccionar medio de pago | `pagos` — Selección del medio de pago | `PedidoFacade` (tabla de medios) | `SeleccionDeMedioDePago_NoDistingueMayusculas` |
| RF-07 | Medios de pago con Adapter | `pagos` — Medios de pago mediante Adapter | `Pagos/` | `CA07_Nequi_…`, `CA07_Daviplata_…`, `CA07_Tarjeta_…` |
| RF-08 | Pago simulado | `pagos` — Pagos simulados y deterministas | `Pagos/Externos/` | `CA08_FondosInsuficientes_…`, `PagoSimulado_MismoMontoYFondos_…` |
| RF-09 | Validar que exista un producto | `pedidos` — Validación previa al pago | `ValidadorPedido.ValidarAntesDePago` | `CA03_ProductoInvalido_…`, `ValidadorPedido_Valida…` |
| RF-10 | Validar que exista un medio de pago | `pedidos` — Validación previa al pago | `ValidadorPedido.ValidarAntesDePago` | `MedioDePagoInexistente_RechazaElPedido_SinCobrar` |
| RF-11 | Validar el resultado del pago | `pedidos` — Validación del resultado del pago | `ValidadorPedido.ValidarPago` | `CA08_PagoRechazado_…`, `ValidadorPedido_Valida…` |
| RF-12 | Confirmar solo con pago aprobado | `pedidos` — Confirmación solo con pago aprobado | `Pedido.Confirmar` | `CA09_PagoAprobado_…`, `Confirmar_FueraDeOrden_…` |
| RF-13 | Facade coordina el proceso | `pedidos` — Facade coordina el proceso completo | `Pedidos/PedidoFacade.cs` | `CA10_Facade_EjecutaElProcesoCompleto…` |
| RF-14 | Mostrar el resultado final | `pedidos` — Resultado final visible | `Pedido.ObtenerResumen`, `Program.cs` | `RF14_ResultadoFinal_MuestraEstadoDescripcionTotalYMensaje` |

## Criterios de aceptación

| CA | Criterio | Prueba |
|---|---|---|
| CA-01 | Crear Café | `ProductosTests.CA01_CrearCafe_DevuelveCafeValido` |
| CA-02 | Crear otros productos | `ProductosTests.CA02_CrearOtrosProductos_DevuelveElProductoCorrespondiente` (Capuchino, Latte, Chocolate) |
| CA-03 | Producto inválido | `ProductosTests.CA03_ProductoInvalido_RechazaDeFormaControlada_SinCobrar` |
| CA-04 | Producto sin complementos | `PersonalizacionTests.CA04_ProductoSinComplementos_PrecioIgualAlBase` |
| CA-05 | Agregar complemento | `PersonalizacionTests.CA05_AgregarUnComplemento_AumentaElPrecio`, `CA05_CadaComplemento_SumaSuPrecioConfigurado` |
| CA-06 | Varios complementos | `PersonalizacionTests.CA06_VariosComplementos_SumaTodos` |
| CA-07 | Pago con interfaz común | `PagosTests.CA07_Nequi_…`, `CA07_Daviplata_…`, `CA07_Tarjeta_…` |
| CA-08 | Pago rechazado | `PagosTests.CA08_FondosInsuficientes_…`, `PedidosTests.CA08_PagoRechazado_ElPedidoNoQuedaConfirmado` |
| CA-09 | Pago aprobado | `PedidosTests.CA09_PagoAprobado_…`, `CA09_LosTresMediosDePago_…` |
| CA-10 | Facade | `PedidosTests.CA10_Facade_EjecutaElProcesoCompletoSinConocerLosDetalles` |

## Pruebas mínimas exigidas (§22 de la especificación base)

| Prueba exigida | Cubierta por |
|---|---|
| Creación de productos | `CA01_…`, `CA02_…` |
| Rechazo de producto inválido | `CA03_…` |
| Producto sin complementos | `CA04_…` |
| Producto con un complemento | `CA05_…` |
| Producto con varios complementos | `CA06_…` |
| Cálculo correcto del precio | `CA05_CadaComplemento_…`, `CA06_…`, `Facade_AplicaLosComplementos…` |
| Procesamiento de Nequi | `CA07_Nequi_…` |
| Procesamiento de Daviplata | `CA07_Daviplata_…` |
| Procesamiento de Tarjeta | `CA07_Tarjeta_…` |
| Pago rechazado | `CA08_…` (pagos y pedidos) |
| Confirmación de pedido | `CA09_…` |
| Uso de Facade | `CA10_…` |

## Estados del pedido (§14)

| Estado | Cómo se alcanza | Prueba |
|---|---|---|
| `CREADO` | Al crear el `Pedido` | `Estados_CaminoExitoso_…` |
| `PERSONALIZADO` | `Pedido.Personalizar` | `Estados_CaminoExitoso_…`, `Confirmar_FueraDeOrden_…` |
| `PAGO_PROCESADO` | `Pedido.MarcarPagoProcesado` (solo con pago aprobado) | `Estados_CaminoExitoso_…` |
| `CONFIRMADO` | `Pedido.Confirmar` | `CA09_…` |
| `RECHAZADO` | `Pedido.Rechazar` (validación o pago fallido) | `CA03_…`, `CA08_…` |

## Reglas de OpenSpec (§23) y dónde se cumplen

| Regla | Dónde se cumple |
|---|---|
| Exactamente un patrón creacional | Solo `Creadores/` (Factory Method). Los diccionarios del Facade son tablas de búsqueda |
| Exactamente tres estructurales | Decorator (`Complementos/`), Adapter (`Pagos/`), Facade (`Pedidos/PedidoFacade.cs`) |
| No agregar funcionalidades fuera de alcance | Sin base de datos, API, login, inventario, etc. (ver `proposal.md`) |
| Código en `src/`, pruebas en `tests/` | Estructura del repositorio |
| Revisar proposal, specs, design y tasks antes de implementar | `openspec/changes/sistema-pedidos-cafeteria/` |
