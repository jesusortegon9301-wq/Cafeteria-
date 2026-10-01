# Verificación del cambio `sistema-pedidos-cafeteria` (`/opsx:verify`)

## Alcance de esta verificación

Fue una **verificación estática**: revisión del código y de las pruebas contra la especificación
base, `proposal.md`, `specs/`, `design.md` y `tasks.md`.

**No se ejecutó `dotnet build` ni `dotnet test`**: el entorno donde se implementó no tiene el SDK de
.NET ni acceso a NuGet. Por eso ninguna prueba se ha visto pasar todavía. El veredicto final
depende de ejecutar los comandos de la sección "Pendiente de ejecución".

Leyenda: ✅ cumple por revisión · ⏳ falta ejecutarlo · ⚠️ observación

## Checklist de la especificación base (§26)

| Punto | Resultado | Evidencia |
|---|---|---|
| **Problema** | ✅ | El flujo `Producto → Complementos → Precio → Pago → Validación → Confirmación` está implementado de punta a punta en `PedidoFacade.RealizarPedido`, con la responsabilidad repartida en clases separadas |
| **Factory Method** | ✅ | `ProductoCreator.CrearProducto()` es abstracto (`Creadores/ProductoCreator.cs:12`). El Facade crea los productos solo mediante `Buscar(_creadores, producto)?.CrearProducto()` (`PedidoFacade.cs:57`). Ninguna línea de `Pedidos/` ni de `Program.cs` hace `new Cafe(...)`, `new Latte(...)`, etc. |
| **Decorator** | ✅ | `ProductoDecorator` es un `Producto` y contiene un `Producto` (`ProductoDecorator.cs:10,12`). El precio se acumula recursivamente (`:24`). No existe ninguna clase por combinación (`CafeConLeche…`) |
| **Adapter** | ✅ | `NequiAdapter`, `DaviplataAdapter` y `TarjetaAdapter` implementan `IPayment`. Envuelven APIs simuladas con firmas y resultados distintos (`bool`, `string`, `int`). El flujo del Facade usa solo `IPayment` (`PedidoFacade.cs:81`) |
| **Facade** | ✅ | `PedidoFacade.RealizarPedido` es la única operación que usa `Program`. Coordina creación, decoración, pago y validación sin calcular ni validar por sí mismo |
| **Requisitos** | ✅ / ⏳ | RF-01 a RF-14 tienen implementación y prueba asociada (ver `docs/03-trazabilidad.md`) |
| **Criterios de aceptación** | ⏳ | CA-01 a CA-10 tienen una prueba con su nombre. Falta ejecutarlas |
| **Código en `src/`** | ✅ | 0 archivos `.cs` fuera de `src/` y `tests/` |
| **Pruebas** | ✅ / ⏳ | 26 métodos de prueba en 4 archivos (32 casos con los `Theory`), que cubren las 12 pruebas mínimas del §22. Falta ejecutarlas |
| **README** | ✅ | `README.md` incluye los 14 apartados pedidos en el §21 |
| **Alcance** | ✅ | Búsqueda de base de datos, HTTP, login, inventario, facturación, Docker, aleatoriedad y fechas: sin resultados. La aplicación no tiene paquetes NuGet |

## Estado por requisito funcional

| RF | Estado | RF | Estado |
|---|---|---|---|
| RF-01 | ✅ | RF-08 | ✅ |
| RF-02 | ✅ | RF-09 | ✅ |
| RF-03 | ✅ | RF-10 | ✅ |
| RF-04 | ✅ | RF-11 | ✅ |
| RF-05 | ✅ | RF-12 | ✅ |
| RF-06 | ✅ | RF-13 | ✅ |
| RF-07 | ✅ | RF-14 | ✅ (`Pedido.ObtenerResumen`, con prueba `RF14_…`) |

## Diferencias entre la especificación y la implementación

1. **Orden de los pasos 7 a 9 del Facade.** `design.md` se ajustó para reflejar el orden real:
   procesar el pago, validar su resultado y solo entonces marcar `PAGO_PROCESADO` y confirmar.
   El resultado observable es el mismo. *Ya está corregido en `design.md`.*
2. **`Pedido.ObtenerResumen` (RF-14).** No estaba en el diseño inicial. Se agregó para que
   `Program` no componga el texto y para poder probar RF-14. Se añadió primero el escenario a
   `specs/pedidos/spec.md` y se actualizó `design.md`.
3. **Cantidad de pruebas.** Un mensaje anterior habló de 26 y luego de 25 métodos; el conteo real
   tras esta verificación es **26 métodos** (32 casos).

## Observaciones (no bloquean)

- ⚠️ **El Facade conoce los adaptadores concretos** en su constructor por defecto
  (`new NequiAdapter()`, etc.), y también conoce los creadores y decoradores concretos en sus
  tablas de búsqueda. Es coherente con el diseño (las tablas son configuración y no hay fábricas
  con nombre propio), pero es el punto que más conviene explicar en una defensa. El cliente
  (`Program`) sí queda aislado de todos ellos.
- ⚠️ **Repetir un complemento** (por ejemplo dos Leches) está permitido por diseño y documentado en
  `design.md`, pero no tiene prueba propia.
- ⚠️ **La impresión en consola** se verifica con `ObtenerResumen` (probado), pero el menú
  interactivo de `Program` solo se puede comprobar ejecutando la aplicación.
- ⚠️ **Mensajes de pago con números** (`{monto}`) dependen de la cultura del sistema. Ninguna
  prueba compara esos textos de forma exacta.
- ⚠️ El escenario "El Facade no contiene la lógica de los componentes" se verifica por revisión de
  código, no con una prueba automatizada.

## Riesgos de compilación revisados a mano

Al no poder compilar, se revisaron puntualmente los puntos donde un compilador suele quejarse:

- Inferencia de `Buscar<T>` con `Dictionary<string, T>` para creadores, complementos y medios.
- Constructor `Dictionary(IEnumerable<KeyValuePair<…>>, IEqualityComparer<…>)` al copiar los medios
  inyectados.
- Flujo de nulabilidad en `RealizarPedido` (`errores.Count > 0 || medio is null`).
- `[DoesNotReturn]` en la función local `Terminar()` de `Program.cs`.
- Llaves, paréntesis y corchetes balanceados en los 33 archivos `.cs`.

Si alguno falla, el mensaje del compilador indicará el archivo y la línea exactos.

## Pendiente de ejecución

Desde la raíz del repositorio:

```bash
dotnet build
dotnet test
dotnet run --project src/Cafeteria
```

Resultado esperado:

- `dotnet build`: sin errores.
- `dotnet test`: 26 métodos, 32 casos, todos en verde.
- `dotnet run`: elegir Café → Leche → Nequi muestra `Estado: CONFIRMADO` y un total de 4.000 COP.

Cuando esos tres pasos sean correctos, se pueden marcar las tareas 1.4, 4.2, 6.2, 8.3, 15.1 y 15.2
de `tasks.md` y archivar el cambio con `/opsx:archive`.

## Conclusión

La implementación **corresponde con la especificación** según la revisión estática: los cuatro
patrones están presentes y son demostrables, todos los requisitos y criterios tienen código y
prueba, el alcance se respetó y las diferencias encontradas están documentadas. **La verificación
queda incompleta hasta ejecutar `dotnet build` y `dotnet test`.**
