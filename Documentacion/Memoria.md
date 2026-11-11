# Memoria técnica · Práctica 1

## 1. Interpretación y alcance

El escenario es una tienda multimarca que quiere enseñar la gama Polestar y simular el acceso de un cliente. Los recorridos prioritarios son detectar una novedad o una oferta, localizar un vehículo por nombre y completar o corregir la identificación. Los precios y la campaña son ejemplos didácticos. El catálogo cubre las cinco carrocerías que lista Polestar España para flotas, como referencia de la gama española de turismos en el escenario de venta.

| Requisito del enunciado | Resolución | Localización |
|---|---|---|
| Novedades, mejor precio y campaña | Polestar 5, precio de entrada del Polestar 2 y promoción de otoño identificada como ficticia | HomeView.xaml |
| Identificación y errores | Campo de contraseña enmascarado, validación sintáctica, feedback y acceso simulado | LoginView.xaml, LoginView.xaml.cs, DemoAccess.cs |
| Galería de todos los productos | Cinco fichas en dos columnas, visibles por desplazamiento, incluyendo las dos carrocerías del Polestar 4 | GalleryView.xaml |
| Logotipo y menú comunes | Wordmark y símbolo vectorial reinterpretado, menú de tres opciones con selección persistente | MainWindow.xaml |
| Animación moderada | Fundido de 0,82 a 1 y desplazamiento vertical de 8 píxeles en 200 ms, sin bucle | MainWindow.xaml.cs |
| Justificación y reproducibilidad | Memoria, casos automatizados, protocolo manual, registro de IA y estado de publicación | Documentacion/ |

## 2. Arquitectura y flujo

App inicia MainWindow y carga Theme.xaml. La ventana mantiene el encabezado común con un menú desplegable y aloja tres UserControl. El controlador del menú muestra una vista y colapsa la anterior para que los controles ocultos queden fuera del recorrido de foco. La pantalla de inicio emite GalleryRequested y la ventana activa la misma opción de menú que usaría el usuario. Inicio es la selección inicial. Cada sección permite volver a cualquiera de las otras dos sin pasar por identificación.

La pantalla de identificación separa los campos de la cuenta de prueba y usa la imagen de montaña existente como fondo decorativo de toda la ventana. El formulario delega reglas en DemoAccess.Check, que devuelve mensaje y campo a corregir. Primero valida ausencia y formato de correo, luego ausencia de contraseña y finalmente la cuenta fija. LoginView borra la contraseña tras cada intento y reinicia el formulario al salir. La coincidencia del correo ignora mayúsculas y espacios exteriores. La contraseña se compara literalmente. El éxito informa en la misma pantalla. No existe persistencia, servidor, registro de credenciales ni autorización real. El catálogo y las promociones son contenido estático editable en XAML.

## 3. Decisiones de interacción

- **Jerarquía y consistencia.** El encabezado muestra el logotipo centrado y el botón de menú. El panel desplegable conserva el orden de las tres secciones y combina iconos con nombres explícitos. Los títulos, textos y fichas comparten estilos. La sección activa combina un fondo destacado con una marca vertical. El foco de teclado tiene un contorno propio. El menú conserva controles de selección nativos con una presentación personalizada.
- **Escaneo y escala.** Dos fichas por fila aprovechan el espacio de escritorio. Las imágenes JPEG conservan su proporción y cada ficha incluye el nombre y el tipo de carrocería. Las tres vistas permiten desplazamiento vertical y los párrafos se ajustan al ancho.
- **Prevención y recuperación.** Etiquetas permanentes, cuenta de ejemplo visible y máscara de contraseña. Los errores de entrada indican qué corregir y devuelven el foco al campo pertinente. La combinación incorrecta usa un mensaje común. El resultado aparece en texto y dispone de región viva de automatización.
- **Teclado y contraste.** Los controles nativos conservan foco y teclado. Las etiquetas tienen teclas de acceso y asociación de automatización. Intro activa el botón de acceso desde el formulario. La interfaz usa carbón, superficies oscuras, texto claro y un acento cálido para acciones y foco. Al activar alto contraste, la aplicación usa los colores del sistema para superficies, texto, contornos y acciones. Las recreaciones de coches son decorativas y sus nombres figuran como texto.
- **Movimiento.** La reducción de movimiento está disponible en el menú común desde cualquier sección. También se omite la animación cuando Windows desactiva animaciones de área cliente o usa alto contraste. Al cambiar rápidamente de pantalla se cancela el efecto anterior.
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

En el rediseño visual se compiló y ejecutó el nuevo paquete Release. Se vio la portada con su imagen, se abrió la galería con su botón y se observaron las primeras cuatro fichas en dos columnas. Se abrió Identificación y el envío vacío mostró su mensaje y devolvió el foco al correo. Se repitió el script de carga parcial XAML y pasaron los seis casos del validador. Las comprobaciones anteriores en Blend y con Narrador corresponden a la versión previa.

Se preparó una segunda revisión visual con tema oscuro, navegación en pestañas y un panel de identificación con foto lateral. Se compiló en Release y se ejecutó. Se inspeccionaron Inicio, Galería e Identificación, se probó el acceso desde la portada y se observó el mensaje de correo vacío y el foco visible. En la compilación final se probó Alt+D, la escritura de correo y contraseña, Tab, máscara de contraseña e Intro con la cuenta de prueba, y se observó «Identificación correcta». El script volvió a cargar las cuatro superficies XAML y pasaron los seis casos aislados. La conmutación real de alto contraste y la prueba de Narrador en esta revisión quedan pendientes.

La revisión con barra lateral se compiló en Release y se ejecutó. Se vio la sección activa en Inicio, se abrió Galería desde su botón y se comprobó que la selección se actualizaba. Alt+D abrió Identificación e Intro con el correo vacío mostró el error. Volvieron a pasar las cuatro comprobaciones parciales de XAML y los seis casos del validador. Los iconos del menú son trazados XAML propios en Theme.xaml.

La versión de entrega recupera el catálogo de estudio de dos columnas y el menú lateral. Se recompiló el ejecutable y se repitieron la carga parcial de las cuatro vistas y los seis casos del validador.

En la revisión de Inicio se compiló y ejecutó la portada panorámica. Se comprobó el coche completo, la lectura del título y el botón de acceso a Galería. Volvieron a pasar las cuatro comprobaciones parciales de XAML y los seis casos del validador.

Las tarjetas de Galería abren una ficha en la misma ventana. Muestra la recreación en grande, la versión técnica y tres métricas: precio simulado, autonomía WLTP y potencia. Volver o pulsar Esc devuelve el foco a la tarjeta de origen y recupera la posición de desplazamiento. Se compiló y se probó la ficha del Polestar 2 con ratón y teclado. También se abrió la ficha del Polestar 5 con Intro y se comprobó el regreso a su tarjeta en la última fila.

En la revisión del encabezado se compiló y ejecutó la aplicación. Se comprobó el logotipo centrado, la apertura del menú con Alt+M y con clic, el cierre con Esc y el cambio a Galería al seleccionar su entrada. Se abrió la ficha del Polestar 2 y se comprobó la etiqueta «Precio». Los importes siguen siendo ficticios y se identifican como tales en esta memoria. Pasaron las cuatro cargas parciales XAML y los seis casos del validador.

Se extendió la imagen de Inicio detrás del encabezado y del contenido, con un degradado para preservar el contraste del texto. Se compiló y ejecutó el prototipo, se inspeccionó el coche en pantalla y se comprobó el acceso a Galería y el regreso a Inicio. Pasaron las cuatro cargas parciales XAML y los seis casos del validador.

Se extendió la imagen de Identificación detrás del formulario y el encabezado. Se compiló y se comprobó en ejecución que Alt+D abre la vista y que Intro con el correo vacío sigue mostrando el error y devolviendo el foco al campo. Pasaron las cargas parciales XAML y los seis casos del validador.

En la auditoría de entrega se extrajo y compiló la versión final en Release. Pasaron las cuatro cargas parciales de XAML, seis casos aislados del validador y 16 comprobaciones integradas sobre la aplicación compilada. Estas últimas comprobaron Inicio, apertura y cierre del menú, acceso a Galería, las cinco fichas (incluidas imagen y cifras), regreso al catálogo, error de correo vacío y acceso válido con borrado de contraseña. No equivalen a una prueba de Narrador o alto contraste en la versión final.

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
| Imágenes de vehículos | Seis recreaciones fotorrealistas generadas con ChatGPT y guardadas como JPEG en Proyecto/Assets. Sus fuentes XAML indican dónde se usa cada imagen | Recursos generados para el prototipo. Son aproximaciones de diseño, sin fotografías oficiales incorporadas |
| Identidad | Nombre comercial Polestar y símbolo vectorial reinterpretado en MainWindow para identificar el encargo | Polestar conserva sus derechos de marca. Uso identificativo académico |
| Tipografía | Segoe UI, instalada con Windows y referenciada en MainWindow.xaml | Fuente de sistema. El ZIP no distribuye archivos tipográficos |
| Herramientas y bibliotecas | .NET 8, WPF, PowerShell 7 y Add-Type. Blend es el entorno previsto para edición y verificación | Componentes del entorno. Sin dependencias NuGet de terceros ni plantillas externas |
| IA | ChatGPT y Codex en ChatGPT Work | Véase el registro de uso. Incluye instrucciones, resultados y revisión pendiente |

La distribución propuesta consiste en los archivos editables. El estudiante publicó el proyecto y el prototipo. Los enlaces figuran en [Evidencias.md](Evidencias.md) y en el [repositorio público](https://github.com/diegotomass7/practica-1-ing-interfaces-polestar).


## Datos técnicos de las fichas

Los precios son de demostración y el precio del Polestar 2 coincide con Inicio. La autonomía WLTP y la potencia se tomaron de una misma variante por modelo. En el Polestar 5, Polestar publica la cifra como objetivo de autonomía.

- [Polestar 2 Standard range Single motor](https://www.polestar.com/es/polestar-2/specifications/)
- [Polestar 3 Rear motor](https://www.polestar.com/es/polestar-3/specifications)
- [Polestar 4 coupé Rear motor](https://www.polestar.com/es/polestar-4-models/polestar-4-coupe/specifications/)
- [Polestar 4 SUV Rear motor](https://www.polestar.com/es/polestar-4-models/polestar-4-suv/specifications/)
- [Polestar 5 Dual motor](https://www.polestar.com/es/polestar-5/specifications/)
