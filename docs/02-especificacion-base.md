# SISTEMA DE PEDIDOS PARA CAFETERÍA

## Especificación base para OpenSpec Dev

> Documento original entregado como base del proyecto. Se conserva sin modificar.
> Las decisiones de diseño derivadas están en
> [`openspec/changes/sistema-pedidos-cafeteria/`](../openspec/changes/sistema-pedidos-cafeteria/).

## 1. Contexto académico

El proyecto consiste en desarrollar una solución pequeña en C# para representar el proceso de creación de un pedido en una cafetería.

El objetivo académico es demostrar el uso de:

### 1 patrón creacional

**Factory Method**

### 3 patrones estructurales

**Decorator**

**Adapter**

**Facade**

La solución debe ser sencilla, clara y fácil de comprender y defender.

No se debe construir un sistema completo de administración de cafeterías.

---

# 2. Problema

Una cafetería necesita representar el proceso de creación de un pedido.

El usuario debe poder seleccionar un producto, agregar complementos, calcular el precio final, seleccionar un medio de pago y confirmar el pedido.

El problema de diseño aparece porque existen diferentes tipos de productos, diferentes complementos y diferentes medios de pago.

Si el código principal crea directamente cada objeto y conoce todos los detalles de cada componente, el sistema puede quedar acoplado y ser difícil de ampliar.

Se requiere una solución pequeña que permita separar estas responsabilidades mediante patrones de diseño.

---

# 3. Usuario

El sistema será utilizado por un usuario que realiza un pedido en la cafetería.

El usuario podrá:

* Seleccionar un producto.
* Agregar complementos.
* Consultar el precio.
* Seleccionar un medio de pago.
* Procesar el pago.
* Confirmar el pedido.

No se requiere implementar:

* Registro de usuarios.
* Login.
* Roles.
* Permisos.
* Gestión de clientes.

---

# 4. Objetivo general

Desarrollar una solución sencilla en C# que permita gestionar un pedido de cafetería y demostrar claramente la aplicación de un patrón creacional y tres patrones estructurales.

La solución debe demostrar:

```text
Factory Method
      +
Decorator
      +
Adapter
      +
Facade
```

---

# 5. Flujo principal

El proceso completo será:

```text
Seleccionar producto
        ↓
Factory Method
        ↓
Producto creado
        ↓
Agregar complementos
        ↓
Decorator
        ↓
Calcular precio final
        ↓
Seleccionar medio de pago
        ↓
Adapter
        ↓
Procesar pago simulado
        ↓
Facade coordina el proceso
        ↓
Validar pedido
        ↓
Confirmar pedido
        ↓
Mostrar resultado
```

---

# 6. Productos

Los productos iniciales serán:

* Café.
* Capuchino.
* Latte.
* Chocolate.

Cada producto debe tener:

* Nombre.
* Descripción.
* Precio base.

Los precios deben estar definidos como valores de prueba para el ejercicio.

No se requiere base de datos.

---

# 7. Complementos

Los complementos iniciales serán:

* Leche.
* Crema.
* Caramelo.
* Azúcar.

Cada complemento tendrá un precio.

El sistema debe permitir:

* Producto sin complementos.
* Producto con un complemento.
* Producto con varios complementos.

La fórmula será:

```text
Precio final =
Precio base + suma de complementos
```

---

# 8. Medios de pago

Los medios de pago serán:

* Nequi.
* Daviplata.
* Tarjeta.

Los pagos serán simulados.

No se deben realizar transacciones reales.

No se deben crear integraciones con:

* Bancos.
* Nequi.
* Daviplata.
* Pasarelas de pago.

El objetivo es demostrar Adapter.

---

# 9. Factory Method

## Problema

El código cliente no debe crear directamente cada tipo concreto de producto.

## Solución

Factory Method debe encargarse de la creación de los productos.

Debe existir una abstracción común para los productos.

El cliente debe trabajar con dicha abstracción.

## Productos

Factory Method debe permitir crear:

```text
Cafe
Capuchino
Latte
Chocolate
```

## Ventaja

El código cliente queda desacoplado de las clases concretas de los productos.

---

# 10. Decorator

## Problema

Un producto puede tener diferentes combinaciones de complementos.

No se deben crear clases para cada combinación posible.

Por ejemplo, no crear:

```text
CafeConLeche
CafeConCrema
CafeConLecheYCrema
CafeConLecheCremaYCaramelo
```

## Solución

Utilizar Decorator para agregar complementos dinámicamente.

Ejemplo:

```text
Cafe
 ↓
Cafe + Leche
 ↓
Cafe + Leche + Crema
 ↓
Cafe + Leche + Crema + Caramelo
```

Cada complemento debe aumentar el precio correspondiente.

---

# 11. Adapter

## Problema

Los diferentes medios de pago pueden tener diferentes formas de procesar una operación.

El sistema necesita trabajar con una interfaz común.

## Solución

Utilizar Adapter.

Conceptualmente:

```text
IPayment
   │
   ├── NequiAdapter
   ├── DaviplataAdapter
   └── TarjetaAdapter
```

Todos deben poder ser utilizados mediante una operación común.

El código principal no debe depender directamente de las diferencias de cada medio de pago.

---

# 12. Facade

## Problema

Completar un pedido implica varios pasos:

```text
Crear producto
Agregar complementos
Calcular precio
Seleccionar pago
Procesar pago
Validar
Confirmar
```

El cliente no debe conocer toda esta complejidad.

## Solución

Crear una fachada que coordine el proceso completo.

Conceptualmente:

```text
PedidoFacade
     │
     ├── Factory Method
     ├── Decorator
     ├── Adapter
     ├── Validación
     └── Confirmación
```

El cliente deberá poder realizar el proceso mediante una interfaz sencilla.

---

# 13. Requisitos funcionales

## RF-01

El sistema debe permitir seleccionar un producto.

## RF-02

El producto debe ser creado utilizando Factory Method.

## RF-03

El sistema debe permitir agregar cero o varios complementos.

## RF-04

Los complementos deben implementarse mediante Decorator.

## RF-05

El sistema debe calcular correctamente el precio final.

## RF-06

El sistema debe permitir seleccionar un medio de pago.

## RF-07

Los medios de pago deben utilizar Adapter.

## RF-08

El pago debe ser simulado.

## RF-09

El sistema debe validar que exista un producto.

## RF-10

El sistema debe validar que exista un medio de pago.

## RF-11

El sistema debe validar el resultado del pago.

## RF-12

El pedido solamente puede confirmarse cuando el pago haya sido aprobado.

## RF-13

Facade debe coordinar el proceso completo.

## RF-14

El sistema debe mostrar el resultado final del pedido.

---

# 14. Estados del pedido

El pedido podrá manejar:

```text
CREADO
PERSONALIZADO
PAGO_PROCESADO
CONFIRMADO
RECHAZADO
```

No es necesario guardar estos estados en una base de datos.

---

# 15. Criterios de aceptación

### CA-01 — Crear Café

Dado que el usuario selecciona Café,

cuando solicite crear el producto,

entonces Factory Method debe devolver una instancia válida de Café.

### CA-02 — Crear otros productos

Factory Method debe permitir crear:

* Café.
* Capuchino.
* Latte.
* Chocolate.

### CA-03 — Producto inválido

Si se solicita un producto no soportado, el sistema debe rechazar la operación de manera controlada.

### CA-04 — Producto sin complementos

El precio final debe ser igual al precio base.

### CA-05 — Agregar complemento

Al agregar un complemento, el precio debe aumentar según el valor configurado.

### CA-06 — Varios complementos

Al agregar varios complementos, el precio final debe incluir la suma de todos.

### CA-07 — Pago

Los tres medios de pago deben poder procesarse mediante una interfaz común.

### CA-08 — Pago rechazado

Si el pago es rechazado, el pedido no debe quedar confirmado.

### CA-09 — Pago aprobado

Si el pago es aprobado y todas las validaciones son correctas, el pedido debe quedar confirmado.

### CA-10 — Facade

El cliente debe poder ejecutar el proceso completo utilizando Facade sin conocer los detalles internos de los otros patrones.

---

# 16. Clases principales esperadas

OpenSpec debe diseñar las clases necesarias, manteniendo el proyecto pequeño.

Como punto de partida se consideran:

```text
Producto
Cafe
Capuchino
Latte
Chocolate

ProductoCreator
CafeCreator
CapuchinoCreator
LatteCreator
ChocolateCreator

ProductoDecorator
LecheDecorator
CremaDecorator
CarameloDecorator
AzucarDecorator

IPayment
NequiAdapter
DaviplataAdapter
TarjetaAdapter

Pedido
PedidoFacade
```

OpenSpec puede modificar esta estructura si encuentra una implementación más correcta del patrón Factory Method, siempre que mantenga el objetivo académico.

---

# 17. Diseño esperado

La arquitectura debe ser sencilla.

No crear capas innecesarias.

La prioridad es que se pueda observar claramente:

```text
Cliente
   │
   ▼
PedidoFacade
   │
   ├──────────────► Factory Method
   │
   ├──────────────► Decorator
   │
   ├──────────────► Adapter
   │
   └──────────────► Validación
```

---

# 18. Tecnologías

Utilizar:

* C#.
* .NET.
* Visual Studio.
* OpenSpec Dev.
* Git.

La implementación debe ser pequeña.

---

# 19. Estructura esperada

```text
CafeteriaPatterns/
│
├── openspec/
│   ├── specs/
│   └── changes/
│
├── src/
│
├── tests/
│
└── README.md
```

OpenSpec puede adaptar esta estructura si su configuración actual utiliza otra organización.

---

# 20. Fuera del alcance

NO implementar:

* Inventario.
* Base de datos.
* API REST.
* Swagger.
* Login.
* Registro de usuarios.
* Roles.
* Permisos.
* Clientes.
* Contabilidad.
* Facturación electrónica.
* Nómina.
* Domicilios.
* Reservas.
* Mesas.
* Proveedores.
* Reportes financieros.
* Aplicación móvil.
* Notificaciones.
* Integraciones bancarias reales.
* Transacciones reales.
* Inteligencia artificial.
* Microservicios.
* Docker.
* Kubernetes.

El proyecto debe mantenerse como una solución pequeña para demostrar los patrones.

---

# 21. README.md

Al finalizar la implementación debe existir un README.md que explique:

1. Nombre del proyecto.
2. Problema.
3. Objetivo.
4. Usuario.
5. Factory Method.
6. Decorator.
7. Adapter.
8. Facade.
9. Flujo del sistema.
10. Estructura del proyecto.
11. Requisitos para ejecutar.
12. Instrucciones para ejecutar.
13. Ejemplo de ejecución.
14. Resultado esperado.

---

# 22. Pruebas

Se deben crear pruebas que permitan demostrar como mínimo:

* Creación de productos.
* Rechazo de producto inválido.
* Producto sin complementos.
* Producto con un complemento.
* Producto con varios complementos.
* Cálculo correcto del precio.
* Procesamiento de Nequi.
* Procesamiento de Daviplata.
* Procesamiento de Tarjeta.
* Pago rechazado.
* Confirmación de pedido.
* Uso de Facade.

Las pruebas deben demostrar el comportamiento definido en las especificaciones.

---

# 23. Reglas para OpenSpec

OpenSpec debe respetar las siguientes reglas:

1. No implementar antes de completar la propuesta.
2. No agregar funcionalidades fuera del alcance.
3. No crear una arquitectura innecesariamente compleja.
4. Mantener el código pequeño.
5. Utilizar exactamente un patrón creacional.
6. Utilizar exactamente tres patrones estructurales.
7. Los patrones deben ser visibles y demostrables en el código.
8. El código debe ser comprensible para un estudiante.
9. La implementación debe corresponder con las especificaciones.
10. Las pruebas deben demostrar los criterios de aceptación.
11. README.md debe documentar la solución.
12. Antes de implementar, revisar proposal.md, specs/, design.md y tasks.md.

---

# 24. Flujo obligatorio de OpenSpec

## ETAPA 1 — EXPLORE

Ejecutar:

/opsx:explore

Objetivo:

Mapear el problema y comprender el código existente.

Debe analizar:

* Problema.
* Alcance.
* Estructura del proyecto.
* Código existente.
* Configuración.
* Dependencias.
* Posibles componentes.
* Decisiones pendientes.

No implementar código.

---

## ETAPA 2 — PROPOSE

Ejecutar:

/opsx:propose

Debe preparar:

```text
proposal.md
specs/
design.md
tasks.md
```

### proposal.md

Debe contener:

* Problema.
* Objetivo.
* Alcance.
* Fuera de alcance.
* Patrones seleccionados.
* Justificación.
* Resultado esperado.

### specs/

Debe contener las especificaciones de comportamiento.

Como mínimo:

```text
Productos
Personalización
Pagos
Pedidos
```

Cada especificación debe tener requisitos y escenarios verificables.

### design.md

Debe explicar:

* Clases.
* Interfaces.
* Responsabilidades.
* Relaciones.
* Factory Method.
* Decorator.
* Adapter.
* Facade.
* Flujo del pedido.

### tasks.md

Debe dividir la implementación en tareas pequeñas y verificables.

Ejemplo:

```text
1. Crear estructura del proyecto.
2. Crear abstracción Producto.
3. Implementar Factory Method.
4. Implementar productos concretos.
5. Implementar Decorator.
6. Implementar complementos.
7. Implementar interfaz de pago.
8. Implementar Adapter.
9. Implementar Pedido.
10. Implementar Facade.
11. Implementar validaciones.
12. Crear pruebas.
13. Crear README.
14. Ejecutar verificación.
```

NO implementar código durante esta etapa.

---

# 25. ETAPA 3 — APPLY

Ejecutar:

/opsx:apply

Implementar únicamente las tareas aprobadas en:

```text
tasks.md
```

La implementación debe:

* Crear el código dentro de src/.
* Crear las pruebas.
* Implementar los cuatro patrones.
* Cumplir las especificaciones.
* Cumplir los criterios de aceptación.
* Crear README.md.
* Mantener el alcance original.

No agregar nuevas funcionalidades sin modificar primero la especificación.

---

# 26. ETAPA 4 — VERIFY

Ejecutar:

/opsx:verify

Verificar:

### Problema

Que la solución resuelva el problema planteado.

### Factory Method

Que realmente se utilice para crear los productos.

### Decorator

Que realmente permita agregar complementos dinámicamente.

### Adapter

Que los diferentes medios de pago sean utilizados mediante una interfaz común.

### Facade

Que coordine el proceso completo.

### Requisitos

Que todos los requisitos estén implementados.

### Criterios de aceptación

Que las pruebas demuestren su cumplimiento.

### Código

Que el código esté dentro de src/.

### Pruebas

Que existan pruebas suficientes.

### README

Que explique cómo funciona y cómo ejecutar el proyecto.

### Alcance

Que no se hayan agregado funcionalidades no solicitadas.

Si existe alguna diferencia entre la especificación y la implementación, reportarla claramente.

---

# 27. Resultado final esperado

El resultado final debe ser una solución pequeña de C# que permita demostrar este escenario:

```text
Usuario
   │
   ▼
Selecciona Café
   │
   ▼
Factory Method
   │
   ▼
Café creado
   │
   ▼
Agrega Leche
   │
   ▼
Decorator
   │
   ▼
Precio actualizado
   │
   ▼
Selecciona Nequi
   │
   ▼
Adapter
   │
   ▼
Pago simulado aprobado
   │
   ▼
Facade
   │
   ▼
Validación
   │
   ▼
Pedido CONFIRMADO
```

El proyecto debe ser suficientemente sencillo para que el estudiante pueda explicar y defender cada clase, cada patrón y cada decisión tomada.
