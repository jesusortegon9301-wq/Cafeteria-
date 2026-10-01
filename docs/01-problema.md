# Problema del sistema de pedidos de cafetería

> Documento original entregado como punto de partida (etapa `/opsx:explore`).
> Se conserva sin modificar. La especificación detallada está en
> [`02-especificacion-base.md`](02-especificacion-base.md).

Actualmente, la cafetería necesita una forma organizada de gestionar el proceso completo de un pedido desde la selección del producto hasta su confirmación.

El proceso de un pedido requiere realizar varias operaciones: seleccionar un producto, agregar complementos, calcular el precio final, seleccionar un medio de pago, procesar el pago, validar que la información del pedido sea correcta y finalmente confirmar el pedido.

Cuando estas responsabilidades se manejan directamente dentro de un mismo componente, el proceso puede volverse difícil de mantener, extender y comprender. Además, agregar nuevos productos, complementos o medios de pago podría requerir modificar diferentes partes del sistema.

Por esta razón, se requiere desarrollar un sistema sencillo que permita representar el proceso completo de un pedido de cafetería de forma organizada y utilizando patrones de diseño.

El sistema deberá permitir:

- Seleccionar un producto de la cafetería.
- Crear el producto correspondiente.
- Agregar uno o varios complementos al producto.
- Calcular el precio final del pedido.
- Seleccionar un medio de pago.
- Procesar el pago mediante una interfaz común.
- Validar que el pedido esté completo y sea válido.
- Confirmar el pedido.
- Mostrar al usuario el resultado del proceso.

El sistema utilizará los siguientes patrones de diseño para separar las responsabilidades:

- **Factory Method:** permitirá crear los diferentes productos de la cafetería sin que el código cliente tenga que conocer directamente las clases concretas.
- **Decorator:** permitirá agregar complementos al producto de manera dinámica y calcular su costo adicional.
- **Adapter:** permitirá manejar diferentes medios de pago mediante una interfaz común, ocultando las diferencias entre ellos.
- **Facade:** permitirá coordinar todo el proceso del pedido mediante una interfaz sencilla, ocultando la complejidad interna de los demás componentes.

El sistema estará enfocado únicamente en el proceso completo de pedidos.

No se pretende desarrollar un sistema completo de administración de cafeterías. Por lo tanto, quedan fuera del problema el inventario, la gestión de clientes, usuarios y roles, la contabilidad, la facturación electrónica, los domicilios, las reservas, las mesas, los proveedores y las integraciones bancarias reales.

El objetivo es resolver de manera sencilla y organizada el problema de gestionar un pedido completo de cafetería, demostrando el uso correcto de un patrón creacional y tres patrones estructurales.

## En palabras sencillas

El problema se resume así:

La cafetería necesita organizar el proceso de un pedido completo sin que todas las responsabilidades estén mezcladas en un solo lugar.

El proceso que vamos a solucionar es:

```text
Producto → Complementos → Precio → Pago → Validación → Confirmación
```

Y los patrones se encargan de cada parte:

| Problema | Patrón |
|---|---|
| ¿Cómo crear los productos? | Factory Method |
| ¿Cómo agregar extras? | Decorator |
| ¿Cómo manejar diferentes pagos? | Adapter |
| ¿Cómo coordinar todo el pedido? | Facade |
