# Propuesta: Sistema de pedidos para cafetería

> **Fuentes:** [`docs/01-problema.md`](../../../docs/01-problema.md) (enunciado original) y
> [`docs/02-especificacion-base.md`](../../../docs/02-especificacion-base.md) (especificación base).
> La trazabilidad de cada requisito está en
> [`docs/03-trazabilidad.md`](../../../docs/03-trazabilidad.md).

## Problema

Una cafetería necesita representar el proceso completo de un pedido:

```text
Producto → Complementos → Precio → Pago → Validación → Confirmación
```

Si el código principal crea directamente cada producto, conoce cada complemento y llama de forma
distinta a cada medio de pago, el sistema queda acoplado. Agregar un producto, un complemento o un
medio de pago obligaría a modificar varias partes del código, y el flujo sería difícil de mantener,
extender y comprender.

## Objetivo

Construir una solución pequeña en C# (.NET 8, consola) que gestione un pedido de cafetería de
principio a fin y que permita **observar y defender** el uso de:

- **1 patrón creacional:** Factory Method.
- **3 patrones estructurales:** Decorator, Adapter y Facade.

## Alcance

Incluido:

- Seleccionar y crear un producto (Café, Capuchino, Latte, Chocolate).
- Agregar cero o varios complementos (Leche, Crema, Caramelo, Azúcar).
- Calcular el precio final: `precio base + suma de complementos`.
- Seleccionar un medio de pago (Nequi, Daviplata, Tarjeta) y procesar un pago **simulado**.
- Validar el pedido antes y después del pago.
- Confirmar el pedido solo si el pago fue aprobado.
- Mostrar al usuario el resultado final.
- Estados del pedido: `CREADO`, `PERSONALIZADO`, `PAGO_PROCESADO`, `CONFIRMADO`, `RECHAZADO`
  (solo en memoria).
- Pruebas automatizadas que demuestren los criterios de aceptación.
- `README.md` que documente la solución.

## Fuera de alcance

Inventario, base de datos, API REST, Swagger, login, registro de usuarios, roles, permisos,
clientes, contabilidad, facturación electrónica, nómina, domicilios, reservas, mesas, proveedores,
reportes financieros, aplicación móvil, notificaciones, integraciones bancarias reales,
transacciones reales, inteligencia artificial, microservicios, Docker y Kubernetes.

## Patrones seleccionados

| Problema | Patrón | Tipo |
|---|---|---|
| ¿Cómo crear los productos sin que el cliente conozca las clases concretas? | Factory Method | Creacional |
| ¿Cómo agregar complementos sin una clase por combinación? | Decorator | Estructural |
| ¿Cómo usar Nequi, Daviplata y Tarjeta con una interfaz común? | Adapter | Estructural |
| ¿Cómo coordinar todo el pedido con una interfaz sencilla? | Facade | Estructural |

## Justificación

- **Factory Method.** Existe una abstracción común `Producto` y una jerarquía de creadores
  (`ProductoCreator` y una subclase por producto). Añadir un producto significa añadir un producto
  y su creador, sin tocar el código cliente.
- **Decorator.** Los complementos se combinan libremente. Sin el patrón habría que crear clases
  como `CafeConLecheYCrema` para cada combinación.
- **Adapter.** Cada medio de pago simulado expone una API con firma y resultado distintos.
  El adaptador traduce cada una a la interfaz común `IPayment`.
- **Facade.** Completar un pedido implica al menos siete pasos. El cliente solo invoca una
  operación y no conoce los demás componentes.

Se usa **exactamente un** patrón creacional y **exactamente tres** estructurales. Los mapeos
por nombre (texto → creador, complemento o medio de pago) son diccionarios simples de
configuración y no constituyen otro patrón creacional (ver `design.md`).

## Capacidades (specs)

| Capacidad | Cubre |
|---|---|
| `productos` | RF-01, RF-02 · CA-01, CA-02, CA-03 |
| `personalizacion` | RF-03, RF-04, RF-05 · CA-04, CA-05, CA-06 |
| `pagos` | RF-06, RF-07, RF-08 · CA-07, CA-08 |
| `pedidos` | RF-09 a RF-14 · CA-08, CA-09, CA-10 |

## Resultado esperado

```text
Usuario selecciona Café → Factory Method crea Cafe
  → agrega Leche → Decorator actualiza el precio
  → selecciona Nequi → Adapter procesa el pago simulado (aprobado)
  → Facade valida → Pedido CONFIRMADO
```

Un proyecto pequeño, con el código en `src/`, pruebas en `tests/`, cuatro patrones visibles en el
código y un `README.md` que un estudiante pueda usar para explicar y defender cada decisión.
