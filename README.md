# CafeteriaPatterns

## 1. Nombre del proyecto

**CafeteriaPatterns** — Sistema de pedidos para cafetería con patrones de diseño.

## 2. Problema

Una cafetería necesita organizar el proceso completo de un pedido:

```text
Producto → Complementos → Precio → Pago → Validación → Confirmación
```

Si el código principal crea directamente cada producto, conoce cada complemento y llama de forma distinta a cada medio de pago, el sistema queda acoplado y es difícil de mantener, extender y comprender.

## 3. Objetivo

Desarrollar una solución sencilla en C# (.NET 8, aplicación de consola) que permita gestionar un pedido de cafetería de principio a fin, demostrando el uso de:

- **1 patrón creacional:** Factory Method
- **3 patrones estructurales:** Decorator, Adapter, Facade

## 4. Usuario

El sistema será utilizado por un usuario que realiza un pedido en la cafetería. El usuario puede:

- Seleccionar un producto (Café, Capuchino, Latte, Chocolate)
- Agregar complementos (Leche, Crema, Caramelo, Azúcar)
- Consultar el precio final
- Seleccionar un medio de pago (Nequi, Daviplata, Tarjeta)
- Procesar el pago (simulado)
- Confirmar el pedido

## 5. Factory Method

**Problema:** El código cliente no debe crear directamente cada tipo concreto de producto.

**Solución:** Existe una abstracción común `Producto` y una jerarquía de creadores (`ProductoCreator` y una subclase por producto). El cliente trabaja con la abstracción.

**Ventaja:** Añadir un producto significa añadir un producto y su creador, sin tocar el código cliente.

```text
ProductoCreator (abstract)
    ├── CafeCreator
    ├── CapuchinoCreator
    ├── LatteCreator
    └── ChocolateCreator
```

## 6. Decorator

**Problema:** Un producto puede tener diferentes combinaciones de complementos. No se deben crear clases para cada combinación posible.

**Solución:** Los complementos se envuelven dinámicamente al producto, aumentando su precio.

**Ventaja:** Se pueden combinar libremente sin crear clases como `CafeConLecheYCrema`.

```text
Producto
    └── ProductoDecorator (abstract)
        ├── LecheDecorator
        ├── CremaDecorator
        ├── CarameloDecorator
        └── AzucarDecorator
```

## 7. Adapter

**Problema:** Los diferentes medios de pago tienen formas distintas de procesar una operación. El sistema necesita trabajar con una interfaz común.

**Solución:** Cada medio de pago se adapta a la interfaz común `IPayment`.

**Ventaja:** El código principal no depende directamente de las diferencias de cada medio de pago.

```text
IPayment (interfaz común)
    ├── NequiAdapter
    ├── DaviplataAdapter
    └── TarjetaAdapter
```

## 8. Facade

**Problema:** Completar un pedido implica varios pasos (crear producto, agregar complementos, calcular precio, seleccionar pago, procesar pago, validar, confirmar). El cliente no debe conocer toda esta complejidad.

**Solución:** `PedidoFacade` coordina todo el proceso completo mediante una interfaz sencilla.

**Ventaja:** El cliente solo invoca una operación y no conoce los detalles internos de los demás componentes.

## 9. Flujo del sistema

```text
Seleccionar producto
        ↓
Factory Method crea el producto
        ↓
Agregar complementos (Decorator)
        ↓
Calcular precio final
        ↓
Seleccionar medio de pago
        ↓
Adapter procesa el pago simulado
        ↓
Facade coordina la validación
        ↓
Confirmar pedido
        ↓
Mostrar resultado
```

## 10. Estructura del proyecto

```text
Cafeteria/
│
├── src/
│   └── Cafeteria/
│       ├── Program.cs
│       ├── Cafeteria.csproj
│       ├── Creadores/          ← Factory Method
│       ├── Productos/          ← Productos concretos
│       ├── Complementos/       ← Decorator
│       ├── Pagos/              ← Adapter
│       └── Pedidos/            ← Facade
│
├── tests/
│   └── Cafeteria.Tests/        ← Pruebas xUnit
│
├── docs/                       ← Documentación
│   ├── 01-problema.md
│   ├── 02-especificacion-base.md
│   ├── 03-trazabilidad.md
│   └── 04-verificacion.md
│
├── openspec/                   ← Especificaciones OpenSpec
│   └── changes/
│       └── sistema-pedidos-cafeteria/
│
├── Cafeteria.sln
└── README.md
```

## 11. Requisitos para ejecutar

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/)
- Git (opcional, para clonar el repositorio)

## 12. Instrucciones para ejecutar

**Desde Visual Studio:**
1. Abrir `Cafeteria.sln`
2. Seleccionar el proyecto `Cafeteria` como proyecto de inicio
3. Presionar `F5` o clic en "Iniciar"

**Desde la terminal:**
```bash
cd src/Cafeteria
dotnet run
```

**Para ejecutar las pruebas:**
```bash
dotnet test
```

## 13. Ejemplo de ejecución

```
=== CAFETERÍA - NUEVO PEDIDO ===

Selecciona un producto:
  1. Café
  2. Capuchino
  3. Latte
  4. Chocolate
Opción: 1

Selecciona complementos:
  1. Leche
  2. Crema
  3. Caramelo
  4. Azúcar
Números separados por coma (Enter = ninguno): 1,3

Selecciona el medio de pago:
  1. Nequi
  2. Daviplata
  3. Tarjeta
Opción: 1

===== RESULTADO DEL PEDIDO =====
Producto: Café
Complementos: Leche, Caramelo
Precio: $8.500 COP
Pago: Nequi (Aprobado)
Estado: CONFIRMADO
```

## 14. Resultado esperado

El sistema permite al usuario realizar un pedido completo de cafetería de forma organizada, demostrando el uso correcto de:

- **Factory Method** para crear productos sin acoplar el cliente a clases concretas
- **Decorator** para agregar complementos dinámicamente
- **Adapter** para manejar diferentes medios de pago con una interfaz común
- **Facade** para coordinar todo el proceso del pedido con una interfaz sencilla

El proyecto es pequeño, claro y fácil de comprender, permitiendo al estudiante explicar y defender cada decisión de diseño.
