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

## Pruebas automáticas

El proyecto `ProyectoEventos.Tests` (xUnit) prueba las reglas de negocio y los servicios: validaciones de las entidades, eventos, menú, presupuestos (vigencia, aprobación, cobro y vencimiento), pendientes y datos del negocio. Cada prueba usa sus propios repositorios en memoria, así no dependen entre sí ni de los datos de ejemplo.

Se corren desde Visual Studio (**Prueba → Explorador de pruebas → Ejecutar todas**) o desde la carpeta del proyecto:

```bash
dotnet test ProyectoEventos.slnx
```

## Pantallas y funciones

La ventana principal tiene un menú lateral (Inicio, Lugares, Clientes, Servicios, Productos, Eventos, Mi negocio y Salir) y cada sección se abre dentro de la misma ventana. Arriba del menú y en el título de la ventana se muestra el nombre del negocio.

### Inicio
- **Saludo** con el nombre del negocio ("Buenas tardes, Salón Las Rosas"), según la hora del día.
- **Alerta** (solo si hace falta) cuando hay eventos de los próximos 7 días que todavía no tienen presupuesto aprobado, con sus clientes.
- Tarjeta destacada con el **próximo evento**: cuánto falta, día y hora, cliente, lugar, invitados y horario.
- **Esta semana:** cuántos eventos hay en los próximos 7 días, cuántos están aprobados y cuántos no.
- **Para atender:** cuántos pendientes hay y de qué tipo (sin presupuesto, desactualizado, vencido, esperando respuesta, falta cobrar).
- **Hoy:** el día de la semana y la fecha.
- **Próximos eventos aprobados:** cuánto falta ("Hoy", "Mañana", "En 5 días"), cliente, lugar, fecha, horario de inicio a fin, invitados y total aprobado. Los de los próximos 7 días se resaltan.
- **Para atender:** eventos próximos que necesitan algo, con un detalle de qué pasó:
  - **Sin presupuesto:** el evento se cargó pero no tiene presupuesto.
  - **Desactualizado:** el presupuesto en espera ya no vale (por ejemplo, "Cambió el precio de Bife de chorizo.").
  - **Esperando respuesta:** el presupuesto está vigente y se envió hace cierta cantidad de días; si el negocio tiene validez configurada, indica cuándo vence.
  - **Vencido:** el presupuesto en espera superó los días de validez del negocio ("Vencido el 09/11/2026."). Conviene llamar al cliente o actualizarlo.
  - **Falta cobrar:** el evento es dentro de los próximos 7 días, tiene presupuesto aprobado y todavía no está pagado (indica cuánto falta y si tiene seña).
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
- **Clientes:** nombre (hasta 80 caracteres), DNI (único, hasta 15), mail (con formato válido, hasta 100) y teléfono (hasta 30). La grilla muestra cuántos eventos tiene cada cliente.
- **Lugares:** nombre (único, hasta 60 caracteres), costo base, costo por hora (puede ser 0 si el lugar cobra un precio fijo), capacidad y ubicación (hasta 100 caracteres). El costo del lugar en un evento es `costo base + costo por hora × horas de duración`. No se puede bajar la capacidad por debajo de los invitados de un evento ya cargado en ese lugar.
- **Productos:** nombre (único, hasta 60 caracteres), precio por adulto, precio por niño, categoría (Principal, Guarnición, Bebida, Postre) y restricción alimentaria (Estándar, Celíaco, Vegetariano, Diabético, Hipertenso).
- **Servicios adicionales:** nombre (único, hasta 60 caracteres), descripción (hasta 200) y monto.

### Eventos
- Grilla con número de evento, cliente, lugar, fecha y hora, duración, invitados, estado del presupuesto (con colores), total y **pago** (sin seña, señado o pagado).
- Los eventos que ya se realizaron se muestran en gris y se pueden ocultar con **Ocultar eventos realizados**.
- Buscador por cliente, lugar, fecha, número o estado del presupuesto.
- Acciones: **Nuevo evento**, **Editar datos**, **Editar menú**, **Ver presupuestos** y **Eliminar** (no se puede eliminar un evento con presupuesto aprobado).

### Mi negocio
Datos propios del negocio, que se usan en los tickets y en la interfaz:
- La pantalla abre en **modo lectura** para no cambiar nada sin querer. No queda ningún campo seleccionado al entrar ni al tocar Editar. **Editar** habilita los campos; **Guardar** aplica los cambios y **Cancelar** los descarta (si hay cambios, pregunta antes).
- **Nombre del local** (obligatorio, hasta 60 caracteres), dirección, teléfono / WhatsApp (números, espacios, +, - y paréntesis), mail (con formato válido), CUIT (11 números, con o sin guiones) e Instagram (sin espacios): aparecen en el encabezado del ticket y del comprobante. Los que se dejan vacíos no se imprimen.
- **Mensaje al pie del ticket:** reemplaza el "Gracias por elegirnos" fijo.
- **Validez del presupuesto (días):** el ticket de un presupuesto en espera muestra "Válido hasta", y Presupuestos e Inicio avisan cuándo vence o si ya venció (entre 0 y 365; con 0 no vence).
- **Seña sugerida (%):** al cobrar un presupuesto aprobado sin seña, el campo Seña ya viene calculado con ese porcentaje del total (se puede cambiar). Debe ser menor a 100 %, porque el 100 % es el pago completo.
- A la derecha hay una **vista previa del ticket** que se actualiza mientras se escribe.
- Si no se cargaron datos, el negocio se llama "Mi negocio".

### Alta y edición de un evento (asistente en dos pasos)

**Paso 1 – Datos del evento:** cliente, lugar, fecha y hora, duración (4 horas por defecto) y cantidad de adultos y niños.
- Mientras se cargan los datos se muestra la **capacidad del lugar**, cuántos lugares quedan libres (o cuánto se supera) y si el lugar **ya tiene otro evento en ese horario**, junto con el **costo del lugar** para esa duración.
- Si la fecha elegida ya pasó, se pide confirmación.
- Si se cambiaron datos y se sale sin tocar Siguiente (Volver o cambiar de sección), se pregunta antes de descartarlos.
- La duración debe ser mayor a cero: si se deja en 0, al tocar Siguiente se avisa con un mensaje.
- Si se crea un evento y se sale sin finalizar, se ofrece descartarlo para que no quede a medio cargar.

**Paso 2 – Menú y servicios:**
- Productos agrupados por categoría, con el precio por adulto y por niño, y filtro **"Apto para"** por restricción alimentaria.
- Se marcan uno o más productos y se agregan como platos (cada producto es un plato) con la cantidad de adultos y niños (por defecto, todos los invitados). Antes de agregar se ve el **subtotal estimado**.
- Un plato puede tener más porciones que invitados (para quienes repiten).
- **Editar plato** carga sus cantidades en el formulario; **Guardar cambios** las aplica y **Cancelar edición** las descarta.
- Servicios adicionales: se agregan y quitan del evento.
- Si al salir (Volver, Finalizar o cambiar de sección) quedan productos o servicios marcados sin agregar, o cambios en un plato que se está editando, se pregunta antes de descartarlos.
- **Finalizar** guarda y genera un presupuesto nuevo si el evento cambió desde el último.

### Presupuestos de un evento
- Grilla con número, fecha de generación, fecha del evento e invitados cotizados, costo total, estado, **pago** y **detalle**.
- Cada presupuesto es una foto de lo cotizado: si después cambia el evento, ya no se modifica.
- **Vigencia:** un presupuesto está vigente si es el último generado y no cambiaron ni el evento ni los precios (costo base o por hora del lugar, productos o servicios). Si no, se marca "(desactualizado)" y la columna Detalle explica el motivo, por ejemplo "Cambió el precio de Bife de chorizo." o "Cambiaron los servicios adicionales.".
- **Vencimiento:** cada presupuesto guarda su fecha de vencimiento al generarse, según la validez configurada en Mi negocio en ese momento. Si después se cambia la validez, los presupuestos ya emitidos conservan la suya (la misma que figura en el ticket del cliente). La columna Detalle muestra "Vence el 09/11/2026 (en 15 días)." o, si ya pasó, "Vencido el …" resaltado.
- **Aprobar:** solo se aprueba un presupuesto vigente, que esté en espera o cancelado (por ejemplo, si el cliente se arrepintió). Si ya hay otro aprobado, se pregunta si se reemplaza. Si está vencido, se pregunta si se aprueba igual.
- **Cancelar:** pide confirmación y avisa especialmente si el presupuesto está aprobado.
- **Actualizar presupuesto:** genera uno nuevo con los datos y precios actuales (o avisa que ya está al día).
- **Ver ticket:** muestra en pantalla un ticket con los datos del negocio y todos los datos cotizados. Con **Guardar PDF** se elige dónde guardarlo y se genera el archivo usando la impresora "Microsoft Print to PDF" que trae Windows (si no está instalada, se avisa con un mensaje).
- **Cobro del presupuesto aprobado:** debajo de la grilla se ve el estado del pago (sin seña, señado o pagado, con cuánto falta cobrar) y no hace falta seleccionar nada:
  - **Registrar seña:** se carga el monto y se confirma. Debe ser mayor a cero y menor al total; se registra una sola seña.
  - **Marcar como pagado:** confirma que el cliente pagó lo que faltaba.
  - **Ver comprobante:** comprobante de pago con la seña, el pago del saldo, lo pagado y lo que resta, con **Guardar PDF**. Se abre solo al registrar la seña o el pago.
  - Si se cancela un presupuesto aprobado con seña, se avisa que la seña no se devuelve y queda indicada en el detalle del presupuesto cancelado. Si se aprueba otro presupuesto en su lugar, la seña pasa al nuevo.
  - Un presupuesto pagado no se puede cancelar ni reemplazar.
- El ticket de un presupuesto aprobado con seña o pago muestra también lo pagado y lo que resta.
- El presupuesto aprobado conserva sus precios aunque después cambien.
- Ningún botón se deshabilita: cada acción valida y avisa con un mensaje si no corresponde.

## Reglas de negocio principales

- Un evento debe tener al menos un invitado (adulto o niño).
- Un evento no puede superar la capacidad del lugar ni superponerse con otro evento en el mismo lugar y horario.
- DNI de clientes y nombres de lugares, productos y servicios no se repiten.
- Los montos admiten centavos y ningún campo numérico se corrige solo: si un valor no es válido, se avisa con un mensaje al guardar.
- Los precios de productos y los montos de servicios deben ser mayores a cero; las cantidades no pueden ser negativas y cada plato debe tener al menos un adulto o un niño.
- No se pueden eliminar clientes, lugares, productos o servicios que estén en uso en algún evento.
- Solo puede haber un presupuesto aprobado por evento.
- Un presupuesto vencido solo se aprueba si se confirma expresamente. Un presupuesto aprobado ya no vence.
- Solo se cobra el presupuesto aprobado: una seña (no reembolsable) y luego el pago del resto.

## Tema visual

Hay dos temas con la misma forma de uso: `Tema1` (matcha y carob, el original) y `Tema2` (verde inglés con detalles en latón y títulos en Georgia). Todos los formularios usan `Tema`, y cuál de los dos es se elige en una sola línea de `Forms/Utils/TemaActivo.cs`:

```csharp
global using Tema = ProyectoEventos.Forms.Utils.Tema2;
```

## Formato

Montos y fechas con formato argentino (`$ 4.500,00` y `22/10/2026 20:00`), sin depender de la configuración regional de Windows. Los textos de las enumeraciones (categorías, restricciones y estados) se definen con `[Description]` y se muestran con tildes.

## Estructura del proyecto

El código está organizado en capas:

| Carpeta | Contenido |
|---|---|
| `Domain` | Entidades y reglas de negocio (`Evento`, `Presupuesto`, `Cliente`, `Lugar`, `Producto`, `ServicioAdicional`, `MenuEvento`, `ItemMenu`, `Negocio`). Cada entidad valida sus datos en el constructor y en los setters de sus propiedades. |
| `Repository` | Interfaz de acceso a datos `IRepository<T>`, común a todas las entidades. El servicio pide el próximo id con `ObtenerProximoId` y guarda los cambios con `Actualizar`. |
| `Repository/Memoria` | Implementación en memoria de esa interfaz. Para pasar a una base de datos se agrega otra implementación sin tocar los servicios. |
| `Service` | Casos de uso; reciben los repositorios por constructor. |
| `Controller` | Punto de entrada que usan las pantallas. |
| `Dto` | Datos que viajan entre las pantallas y los servicios. |
| `Forms` | Pantallas de Windows Forms y utilidades de interfaz (`Forms/Utils`: tema visual, grillas, búsqueda, avisos, ticket). |
| `Common` | Formato de montos y fechas, configuración (`Configuracion.DiasProximos`), enumeraciones (`Common/Enums`) y excepciones de negocio. |
| `Datos` | Datos de ejemplo que se cargan en modo Debug. |
| `ProyectoEventos.Tests` | Pruebas automáticas (xUnit) del dominio y los servicios. |
