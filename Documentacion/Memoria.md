# Memoria técnica · Práctica 1

## 1. Interpretación y alcance

El escenario es una tienda multimarca que quiere enseñar la gama Polestar y simular el acceso de un cliente. Los recorridos prioritarios son detectar una novedad o una oferta, localizar un vehículo por nombre y completar o corregir la identificación. Los precios y la campaña son ejemplos didácticos. El catálogo cubre las cinco carrocerías que lista Polestar España para flotas, como referencia de la gama española de turismos en el escenario de venta.

| Requisito del enunciado | Resolución | Localización |
|---|---|---|
| Novedades, mejor precio y campaña | Polestar 5, precio de entrada del Polestar 2 y promoción de otoño identificada como ficticia | HomeView.xaml |
| Identificación y errores | Campo de contraseña enmascarado, validación sintáctica, feedback y acceso simulado | LoginView.xaml, LoginView.xaml.cs, DemoAccess.cs |
| Galería de todos los productos | Cinco fichas visibles por desplazamiento, incluyendo las dos carrocerías del Polestar 4 | GalleryView.xaml |
| Logotipo y menú comunes | Wordmark y símbolo vectorial reinterpretado, menú de tres opciones con selección persistente | MainWindow.xaml |
| Animación moderada | Cambio de opacidad de 0,78 a 1 en 160 ms, sin bucle | MainWindow.xaml.cs |
| Justificación y reproducibilidad | Memoria, casos automatizados, protocolo manual, registro de IA y estado de publicación | Documentacion/ |

## 2. Arquitectura y flujo

App inicia MainWindow y carga Theme.xaml. La ventana mantiene el encabezado común y aloja tres UserControl. El controlador del menú muestra una vista y colapsa la anterior para que los controles ocultos queden fuera del recorrido de foco. La pantalla de inicio emite GalleryRequested y la ventana activa la misma opción de menú que usaría el usuario. Inicio es la selección inicial. Cada sección permite volver a cualquiera de las otras dos sin pasar por identificación.

El formulario delega reglas en DemoAccess.Check, que devuelve mensaje y campo a corregir. Primero valida ausencia y formato de correo, luego ausencia de contraseña y finalmente la cuenta fija. LoginView borra la contraseña tras cada intento y reinicia el formulario al salir. La coincidencia del correo ignora mayúsculas y espacios exteriores. La contraseña se compara literalmente. El éxito informa en la misma pantalla. No existe persistencia, servidor, registro de credenciales ni autorización real. El catálogo y las promociones son contenido estático editable en XAML.

## 3. Decisiones de interacción

- **Jerarquía y consistencia.** El encabezado conserva el orden y la posición del logo y el menú. Los títulos, textos y fichas comparten estilos. El estado seleccionado se distingue con el indicador del control, además de cualquier diferencia de color.
- **Escaneo y escala.** Una ficha por fila permite recorrer el catálogo con nombres explícitos y descripciones breves. Las imágenes vectoriales se escalan con Viewbox. Las tres vistas permiten desplazamiento vertical y los párrafos se ajustan al ancho.
- **Prevención y recuperación.** Etiquetas permanentes, cuenta de ejemplo visible y máscara de contraseña. Los errores de entrada indican qué corregir y devuelven el foco al campo pertinente. La combinación incorrecta usa un mensaje común. El resultado aparece en texto y dispone de región viva de automatización.
- **Teclado y contraste.** Los controles nativos conservan foco y teclado. Las etiquetas tienen teclas de acceso y asociación de automatización. Intro activa el botón de acceso desde el formulario. Los colores funcionales proceden del sistema. Los perfiles de coches son decorativos y sus nombres figuran como texto.
- **Movimiento.** El control común de reducción de movimiento se mantiene visible para que se pueda cambiar desde cualquier sección. También se omite la animación cuando Windows desactiva animaciones de área cliente o usa alto contraste. Al cambiar rápidamente de pantalla se cancela el efecto anterior.
- **Diseño centrado en la tarea.** El catálogo queda abierto desde el principio para que un vendedor pueda mostrar un vehículo sin acceso previo. Se definieron recorridos a partir del escenario del enunciado. La observación con usuarios queda pendiente.

## 4. Verificación y correcciones

Ejecutado el 11 de noviembre de 2026 en Windows. Repetición con `pwsh -NoProfile -STA -File Documentacion/Verificar.ps1`.

- El lector XAML de WPF cargó Theme.xaml y dispuso las tres vistas y la estructura de MainWindow a 600 × 460. La prueba retira x:Class, los manejadores Click y las instancias de vistas de la ventana. Comprueba recursos, propiedades y disposición básica por separado.
- En el primer intento WPF rechazó AutomationProperties.AccessibilityView en las ilustraciones. Se quitó esa propiedad y se repitió con éxito.
- El script compiló y ejecutó DemoAccess de forma aislada. Pasaron seis casos, que comprueban mensaje y campo de corrección, con correo vacío, formato inválido, contraseña vacía, contraseña incorrecta, cuenta incorrecta y cuenta válida con espacios y mayúsculas.
- En la primera sesión solo estaba disponible el runtime. Tras instalar Visual Studio Community 2026, el SDK 11.0.100 compiló la solución dirigida a .NET 8 con cero errores y cero advertencias.
- Se ejecutó la copia extraída desde Visual Studio con F5. Se comprobaron la pantalla inicial, los tres destinos del menú, el encabezado común y las cinco fichas de la galería mediante desplazamiento.
- En Identificación se probó el envío vacío, que mostró «Introduce el correo electrónico» y devolvió el foco al campo. Con la cuenta de prueba se observó el enmascarado, el mensaje de éxito y el borrado de la contraseña. Al salir y volver, el formulario apareció limpio.
- Quedan pendientes las comprobaciones específicas de la tabla que aún no se han ejecutado. Estas pruebas de interfaz fueron realizadas por Codex.

En una segunda ronda, una prueba automatizada instanció la aplicación y la ventana compiladas y disparó los eventos de sus controles. Pasaron 11 comprobaciones, entre ellas el botón Inicio → Galería, el cambio a Identificación, la ausencia de animación al activar la reducción de movimiento, las asociaciones de etiquetas, la región viva, el botón predeterminado, cinco respuestas del formulario y su reinicio. Estas pruebas no reproducen el recorrido con teclas físicas ni lo que anuncia Narrador.

Se abrió la solución del Escritorio en Blend para Visual Studio 2026. El diseñador mostró Inicio, Galería, Identificación y la ventana principal con su encabezado común. En las cuatro vistas el editor indicó «No se encontraron problemas».

Se ejecutó la copia del Escritorio desde Blend con F5. Con el teclado se comprobó el foco con Tab, el acceso a Galería con Intro desde su botón, los atajos de Inicio y Galería, la selección del menú con flecha y Espacio, y el envío vacío con Intro. Inicio e Identificación compartían Alt+I. Se cambió Identificación a Alt+D y se comprobó ese atajo en el ejecutable Release recompilado. La inspección de automatización expuso las etiquetas del formulario y el mensaje de error. El estudiante confirmó que funcionó la prueba con Narrador en Identificación, siguiendo el recorrido del campo de correo y el envío vacío. Se registra como confirmación del estudiante, sin transcripción de audio.

| Comprobación pendiente | Pasos y criterio de aceptación |
|---|---|
| Lector de pantalla | Ampliar la comprobación con Narrador al menú y la galería |
| Escala y contraste | A 100 %, 150 % y 200 %, probar tamaño mínimo, desplazamiento y alto contraste. Los controles, textos y estados deben seguir siendo legibles y alcanzables |
| Movimiento | Cambiar la preferencia de Windows y observar la transición con y sin animaciones de área cliente |
| Sesión con usuarios | Pedir a una persona que encuentre la oferta, localice el 4 SUV y corrija un acceso fallido. Registrar tiempo, errores y observaciones anonimizadas |

## 5. Fuentes y recursos

| Material | Origen y aportación | Condiciones |
|---|---|---|
| Enunciado de Práctica 1 | Material docente de Interfaces de Usuario de la UMH, páginas 2–5 | Consulta interna. Esta memoria y la matriz documentan los requisitos sin redistribuir el original |
| Gama | [Polestar España · Flotas](https://www.polestar.com/es/fleet/) y [Modelos SUV](https://www.polestar.com/es/suv/). Nombres y categorías de vehículos | Referencia factual. Sin reutilización de fotos, textos comerciales ni logotipos descargados |
| Ilustraciones | Trazados y formas XAML generados para este prototipo, incluidos como fuente en HomeView y GalleryView | Sin material gráfico de terceros incorporado |
| Identidad | Nombre comercial Polestar y símbolo vectorial reinterpretado en MainWindow para identificar el encargo | Polestar conserva sus derechos de marca. Uso identificativo académico |
| Tipografía | Segoe UI, instalada con Windows y referenciada en MainWindow.xaml | Fuente de sistema. El ZIP no distribuye archivos tipográficos |
| Herramientas y bibliotecas | .NET 8, WPF, PowerShell 7 y Add-Type. Blend es el entorno previsto para edición y verificación | Componentes del entorno. Sin dependencias NuGet de terceros ni plantillas externas |
| IA | ChatGPT y Codex en ChatGPT Work | Véase el registro de uso. Incluye instrucciones, resultados y revisión pendiente |

La distribución propuesta consiste en los archivos editables. El repositorio y las evidencias públicas requieren la revisión y autorización del titular de la entrega.

