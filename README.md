# Calculadora de Eventos

Aplicación de escritorio (Windows Forms, .NET 10) para armar y presupuestar eventos: se cargan clientes, lugares, productos y servicios adicionales; se arma cada evento con su menú y sus servicios, y el sistema genera los presupuestos, controla su vigencia y permite aprobarlos, cancelarlos e imprimirlos como ticket.

## Requisitos y cómo ejecutarla

- Windows con el SDK de **.NET 10** instalado.
- Abrir `ProyectoEventos.slnx` en Visual Studio y ejecutar, o desde una terminal en la carpeta del proyecto:

```bash
dotnet run
```

Al ejecutar en modo **Debug** se cargan automáticamente datos de ejemplo (clientes, lugares, productos, servicios y eventos con presupuestos en distintos estados) para poder probar todas las pantallas.

> Los datos se guardan en memoria: al cerrar la aplicación se pierden.

## Pantallas y funciones

La ventana principal tiene un menú lateral (Inicio, Lugares, Clientes, Servicios, Productos, Eventos y Salir) y cada sección se abre dentro de la misma ventana.

### Inicio
- Tres tarjetas de resumen: **próximos eventos** aprobados, **eventos esperando aprobación** y **total aprobado del mes** en curso.
- Lista de los próximos eventos aprobados con cliente, lugar, fecha y hora, invitados y total aprobado.

### Clientes, Lugares, Productos y Servicios adicionales
Las cuatro pantallas funcionan igual (alta, edición y baja):
- **Guardar** crea un registro nuevo. Al terminar aparece un aviso breve ("Cliente guardado.") y la fila guardada queda seleccionada en la grilla.
- **Editar** carga el registro seleccionado en el formulario: el título pasa a "Editando: …" y aparece **Cancelar** para descartar los cambios.
- **Eliminar** pide confirmación y no permite borrar datos que están en uso (por ejemplo, un cliente con eventos).
- Los campos obligatorios están marcados con `*`.
- **Buscador** sobre cada grilla: filtra mientras se escribe, sin distinguir mayúsculas ni tildes, y acepta varias palabras.
- Si hay datos escritos sin guardar y se cambia de sección o se edita otro registro, se pregunta antes de descartarlos.
- Cuando la grilla está vacía se muestra una guía de qué hacer.

Datos de cada uno:
- **Clientes:** nombre, DNI (único), mail (con formato válido) y teléfono. La grilla muestra cuántos eventos tiene cada cliente.
- **Lugares:** nombre (único), costo base, capacidad y ubicación. No se puede bajar la capacidad por debajo de los invitados de un evento ya cargado en ese lugar.
- **Productos:** nombre (único), precio por adulto, precio por niño, categoría (Principal, Guarnición, Bebida, Postre) y restricción alimentaria (Estándar, Celíaco, Vegetariano, Diabético, Hipertenso).
- **Servicios adicionales:** nombre (único), descripción y monto.

### Eventos
- Grilla con número de evento, cliente, lugar, fecha y hora, duración, invitados, estado del presupuesto (con colores) y total.
- Los eventos que ya se realizaron se muestran en gris y se pueden ocultar con **Ocultar eventos realizados**.
- Buscador por cliente, lugar, fecha, número o estado del presupuesto.
- Acciones: **Nuevo evento**, **Editar datos**, **Editar menú**, **Ver presupuestos**, **Duplicar** y **Eliminar** (no se puede eliminar un evento con presupuesto aprobado).

### Alta y edición de un evento (asistente en dos pasos)

**Paso 1 – Datos del evento:** cliente, lugar, fecha y hora, duración (4 horas por defecto) y cantidad de adultos y niños.
- Mientras se cargan los datos se muestra la **capacidad del lugar**, cuántos lugares quedan libres (o cuánto se supera) y si el lugar **ya tiene otro evento en ese horario**.
- Si la fecha elegida ya pasó, se pide confirmación.
- Si se crea un evento y se sale sin finalizar, se ofrece descartarlo para que no quede a medio cargar.
- **Duplicar** abre este mismo paso con los datos de otro evento (una semana después, a la misma hora) y copia su menú y sus servicios.

**Paso 2 – Menú y servicios:**
- Productos agrupados por categoría, con el precio por adulto y por niño, y filtro **"Apto para"** por restricción alimentaria.
- Se marcan uno o más productos y se agregan como platos con la cantidad de adultos y niños (por defecto, todos los invitados). Antes de agregar se ve el **subtotal estimado**.
- Un plato puede tener más porciones que invitados (para quienes repiten).
- **Editar plato** carga sus cantidades en el formulario; **Guardar cambios** las aplica y **Cancelar edición** las descarta.
- Servicios adicionales: se agregan y quitan del evento.
- **Finalizar** guarda y genera un presupuesto nuevo si el evento cambió desde el último.

### Presupuestos de un evento
- Grilla con número, fecha de generación, fecha del evento e invitados cotizados, costo total, estado y **detalle**.
- Cada presupuesto es una foto de lo cotizado: si después cambia el evento, ya no se modifica.
- **Vigencia:** un presupuesto está vigente si es el último generado y no cambiaron ni el evento ni los precios (lugar, productos o servicios). Si no, se marca "(desactualizado)" y la columna Detalle explica el motivo, por ejemplo "Cambió el precio de Bife de chorizo." o "Cambiaron los servicios adicionales.".
- **Aprobar:** solo se aprueba un presupuesto vigente, que esté en espera o cancelado (por ejemplo, si el cliente se arrepintió). Si ya hay otro aprobado, se pregunta si se reemplaza.
- **Cancelar:** pide confirmación y avisa especialmente si el presupuesto está aprobado.
- **Actualizar presupuesto:** genera uno nuevo con los datos y precios actuales (o avisa que ya está al día).
- **Imprimir ticket:** vista previa de un ticket de 42 columnas con todos los datos cotizados, listo para imprimir.
- El presupuesto aprobado conserva sus precios aunque después cambien.
- Ningún botón se deshabilita: cada acción valida y avisa con un mensaje si no corresponde.

## Reglas de negocio principales

- Un evento no puede superar la capacidad del lugar ni superponerse con otro evento en el mismo lugar y horario.
- DNI de clientes y nombres de lugares, productos y servicios no se repiten.
- Los precios de productos y los montos de servicios deben ser mayores a cero; las cantidades no pueden ser negativas y cada plato debe tener al menos un adulto o un niño.
- No se pueden eliminar clientes, lugares, productos o servicios que estén en uso en algún evento.
- Solo puede haber un presupuesto aprobado por evento.

## Formato

Montos y fechas con formato argentino (`$ 4.500,00` y `22/10/2026 20:00`), sin depender de la configuración regional de Windows. Los textos de categorías y restricciones se muestran con tildes.

## Estructura del proyecto

El código está organizado en capas:

| Carpeta | Contenido |
|---|---|
| `Domain` | Entidades y reglas de negocio (`Evento`, `Presupuesto`, `Cliente`, `Lugar`, `Producto`, `ServicioAdicional`, `MenuEvento`, `ItemMenu`). |
| `Repository` | Almacenamiento en memoria (`IRepository<T>`, `IEventoRepository` y sus implementaciones). |
| `Service` | Casos de uso; reciben los repositorios por constructor. |
| `Controller` | Punto de entrada que usan las pantallas. |
| `Dto` | Datos que viajan entre las pantallas y los servicios. |
| `Forms` | Pantallas de Windows Forms y utilidades de interfaz (`Forms/Utils`: tema visual, búsqueda, avisos, ticket). |
| `Common` | Enumeraciones y excepciones de negocio. |
| `DatosIniciales.cs` | Datos de ejemplo que se cargan en modo Debug. |
