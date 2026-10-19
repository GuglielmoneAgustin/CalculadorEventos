# Calculadora de Eventos

Aplicación de escritorio (Windows Forms, .NET 10) para armar y presupuestar eventos: se cargan clientes, lugares, productos y servicios adicionales; se arma cada evento con su menú y sus servicios, y el sistema genera los presupuestos, controla su vigencia y permite aprobarlos, cancelarlos y verlos como ticket.

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
- **Alerta** (solo si hace falta) cuando hay eventos de los próximos 7 días que todavía no tienen presupuesto aprobado, con sus clientes.
- Tarjeta destacada con el **próximo evento**: cuánto falta, día y hora, cliente, lugar, invitados y horario.
- **Esta semana:** cuántos eventos hay en los próximos 7 días, cuántos están aprobados y cuántos no.
- **Para atender:** cuántos pendientes hay y de qué tipo (sin presupuesto, desactualizado, esperando respuesta).
- **Hoy:** el día de la semana y la fecha.
- **Próximos eventos aprobados:** cuánto falta ("Hoy", "Mañana", "En 5 días"), cliente, lugar, fecha, horario de inicio a fin, invitados y total aprobado. Los de los próximos 7 días se resaltan.
- **Para atender:** eventos próximos que necesitan algo, con un detalle de qué pasó:
  - **Sin presupuesto:** el evento se cargó pero no tiene presupuesto.
  - **Desactualizado:** el presupuesto en espera ya no vale (por ejemplo, "Cambió el precio de Bife de chorizo.").
  - **Esperando respuesta:** el presupuesto está vigente y se envió hace cierta cantidad de días.
- Un botón **Ver presupuesto** sirve para las dos grillas: abre los presupuestos del evento elegido en cualquiera de ellas (al seleccionar en una grilla se deselecciona la otra). Al volver, Inicio se actualiza.

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
- **Lugares:** nombre (único), costo base, costo por hora (puede ser 0 si el lugar cobra un precio fijo), capacidad y ubicación. El costo del lugar en un evento es `costo base + costo por hora × horas de duración`. No se puede bajar la capacidad por debajo de los invitados de un evento ya cargado en ese lugar.
- **Productos:** nombre (único), precio por adulto, precio por niño, categoría (Principal, Guarnición, Bebida, Postre) y restricción alimentaria (Estándar, Celíaco, Vegetariano, Diabético, Hipertenso).
- **Servicios adicionales:** nombre (único), descripción y monto.

### Eventos
- Grilla con número de evento, cliente, lugar, fecha y hora, duración, invitados, estado del presupuesto (con colores) y total.
- Los eventos que ya se realizaron se muestran en gris y se pueden ocultar con **Ocultar eventos realizados**.
- Buscador por cliente, lugar, fecha, número o estado del presupuesto.
- Acciones: **Nuevo evento**, **Editar datos**, **Editar menú**, **Ver presupuestos**, **Duplicar** y **Eliminar** (no se puede eliminar un evento con presupuesto aprobado).

### Alta y edición de un evento (asistente en dos pasos)

**Paso 1 – Datos del evento:** cliente, lugar, fecha y hora, duración (4 horas por defecto) y cantidad de adultos y niños.
- Mientras se cargan los datos se muestra la **capacidad del lugar**, cuántos lugares quedan libres (o cuánto se supera) y si el lugar **ya tiene otro evento en ese horario**, junto con el **costo del lugar** para esa duración.
- Si la fecha elegida ya pasó, se pide confirmación.
- Si se cambiaron datos y se sale sin tocar Siguiente (Volver o cambiar de sección), se pregunta antes de descartarlos.
- La duración debe ser mayor a cero: si se deja en 0, al tocar Siguiente se avisa con un mensaje.
- Si se crea un evento y se sale sin finalizar, se ofrece descartarlo para que no quede a medio cargar.
- **Duplicar** abre este mismo paso con los datos de otro evento (una semana después, a la misma hora) y copia su menú y sus servicios.

**Paso 2 – Menú y servicios:**
- Productos agrupados por categoría, con el precio por adulto y por niño, y filtro **"Apto para"** por restricción alimentaria.
- Se marcan uno o más productos y se agregan como platos con la cantidad de adultos y niños (por defecto, todos los invitados). Antes de agregar se ve el **subtotal estimado**.
- Un plato puede tener más porciones que invitados (para quienes repiten).
- **Editar plato** carga sus cantidades en el formulario; **Guardar cambios** las aplica y **Cancelar edición** las descarta.
- Servicios adicionales: se agregan y quitan del evento.
- Si al salir (Volver, Finalizar o cambiar de sección) quedan productos o servicios marcados sin agregar, o cambios en un plato que se está editando, se pregunta antes de descartarlos.
- **Finalizar** guarda y genera un presupuesto nuevo si el evento cambió desde el último.

### Presupuestos de un evento
- Grilla con número, fecha de generación, fecha del evento e invitados cotizados, costo total, estado y **detalle**.
- Cada presupuesto es una foto de lo cotizado: si después cambia el evento, ya no se modifica.
- **Vigencia:** un presupuesto está vigente si es el último generado y no cambiaron ni el evento ni los precios (costo base o por hora del lugar, productos o servicios). Si no, se marca "(desactualizado)" y la columna Detalle explica el motivo, por ejemplo "Cambió el precio de Bife de chorizo." o "Cambiaron los servicios adicionales.".
- **Aprobar:** solo se aprueba un presupuesto vigente, que esté en espera o cancelado (por ejemplo, si el cliente se arrepintió). Si ya hay otro aprobado, se pregunta si se reemplaza.
- **Cancelar:** pide confirmación y avisa especialmente si el presupuesto está aprobado.
- **Actualizar presupuesto:** genera uno nuevo con los datos y precios actuales (o avisa que ya está al día).
- **Ver ticket:** muestra en pantalla un ticket con todos los datos cotizados.
- El presupuesto aprobado conserva sus precios aunque después cambien.
- Ningún botón se deshabilita: cada acción valida y avisa con un mensaje si no corresponde.

## Reglas de negocio principales

- Un evento no puede superar la capacidad del lugar ni superponerse con otro evento en el mismo lugar y horario.
- DNI de clientes y nombres de lugares, productos y servicios no se repiten.
- Los montos admiten centavos y ningún campo numérico se corrige solo: si un valor no es válido, se avisa con un mensaje al guardar.
- Los precios de productos y los montos de servicios deben ser mayores a cero; las cantidades no pueden ser negativas y cada plato debe tener al menos un adulto o un niño.
- No se pueden eliminar clientes, lugares, productos o servicios que estén en uso en algún evento.
- Solo puede haber un presupuesto aprobado por evento.

## Formato

Montos y fechas con formato argentino (`$ 4.500,00` y `22/10/2026 20:00`), sin depender de la configuración regional de Windows. Los textos de las enumeraciones (categorías, restricciones y estados) se definen con `[Description]` y se muestran con tildes.

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
