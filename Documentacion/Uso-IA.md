# Uso de inteligencia artificial

El 11 de noviembre de 2026 usé ChatGPT y Codex para la práctica de Interfaces de Usuario. En ChatGPT Plus inicié el trabajo y adjunté el enunciado. En Codex pedí las comprobaciones y ayudas para terminar el trabajo. El generador de imágenes de ChatGPT produjo las recreaciones de los coches y la imagen panorámica de Inicio. Se indica la fecha de uso porque la versión concreta de los modelos no quedó registrada.

## Prompts, resultados y decisiones

Los extractos reproducen mensajes del trabajo. Los puntos suspensivos entre corchetes indican que el mensaje se ha abreviado. El código, las imágenes y la memoria de la entrega permiten revisar los resultados.

| Instrucción | Resultado | Decisión |
|---|---|---|
| «puedes ejecutar polestar, verifca tambien que todo funcione correctamente» | Codex compiló y ejecutó el proyecto. En las revisiones corrigió una propiedad XAML incompatible y un atajo duplicado. | Se incorporaron las correcciones y los resultados de las pruebas a la memoria. |
| «Prefiero también más oscuro» | Codex aplicó una paleta oscura y rediseñó la navegación y el formulario. | Pedí conservar el estilo oscuro y después reunir las opciones en el menú de tres rayas. |
| «Vale, olvídate de todo lo que te he dicho de 360 e imágenes de la calle. Haz lo que teníamos hecho antes […]» | Codex restauró el catálogo en dos columnas con imágenes de estudio. | Pedí retirar el giro y las fotografías de calle y salón y recuperar las imágenes de estudio. |
| «Genera una imagen para el fondo de la pantalla de inicio de una aplicación de Polestar, estilo minimalista escandinavo, coche eléctrico en un entorno moderno con iluminación tenue y elegante» | Se generó una escena panorámica con un coche plateado a la derecha y espacio oscuro a la izquierda. Codex la integró como fondo de Inicio. | Pedí extender la imagen detrás del logotipo y el menú, y aplicar el mismo tratamiento a Identificación con su propia imagen. |
| «Sí, haz eso y por si le quieres añadir algún dato técnico y tal, también […] que quede simple, pero profesional.» | Codex añadió fichas con imagen ampliada, precio, autonomía y potencia. | La ficha indica la variante técnica. Los importes son ficticios y las especificaciones proceden de Polestar España. |

## Imágenes generadas

Para el catálogo, Codex pidió una lámina de seis paneles con acabado fotográfico, sin rótulos ni matrículas. La instrucción especificó una escena de montaña para el Polestar 5 y cinco vistas de estudio de los modelos 2, 3, 4 coupé, 4 SUV y 5. Se recortaron los paneles en JPEG. La imagen de montaña se reutilizó en Identificación.

Para Inicio, Codex pidió una composición panorámica 16:9 con un Polestar 5 plateado completo en la mitad derecha, hormigón oscuro, luz ámbar indirecta y la parte izquierda despejada para los controles. El resultado se exportó a JPEG y se añadió un degradado para mejorar la lectura. Las siete imágenes están en `Proyecto/Assets`. Son recreaciones y pueden diferir de los vehículos comerciales.

## Contribución y revisión

Mi aportación fue facilitar el enunciado, decidir cómo quería que se viera la aplicación y pedir cambios al revisar las distintas versiones. Abrí el proyecto en Blend, comprobé con Narrador el recorrido de Identificación y publiqué la primera versión en GitHub. Codex realizó la implementación en C# y XAML, redactó la documentación, preparó los paquetes y ejecutó las comprobaciones técnicas que figuran en la memoria.

| Aspecto | Evidencia y alcance |
|---|---|
| Corrección y requisitos | Codex contrastó la solución con el PDF, compiló el proyecto y probó los recorridos registrados en la memoria. |
| Usabilidad y accesibilidad | La revisión del estudiante con Narrador se hizo en Identificación en una versión anterior. Codex probó el teclado y el retorno a la tarjeta al cerrar una ficha. La memoria distingue las comprobaciones de cada versión. |
| Seguridad y privacidad | La cuenta es ficticia y la validación es local. El prototipo carece de servidor y almacenamiento de credenciales. |
| Sesgos | Se usaron etiquetas en español, nombres de modelos y campos explícitos. No se registró una evaluación humana específica con usuarios de distintos perfiles. |
| Propiedad intelectual | La memoria identifica los recursos generados y las páginas de Polestar usadas como referencia técnica. El PDF docente se excluye del paquete público. |

La documentación de entrega contiene el [registro de pruebas](Memoria.md) y los [enlaces a los materiales](Evidencias.md).

