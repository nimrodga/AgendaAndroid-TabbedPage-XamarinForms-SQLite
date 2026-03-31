# **AgendaAndroid**
Es un proyecto Xamarin Forms Nativo a manera de ilustrar ligeramente 
la implementación de la librería **Sqlite-net-pcl** en un proyecto Xamarin, 
que aunque ya ha sido sustituido por NETMAUI, la funcionalidad básica aun ofrece 
alternativas de aprendizaje, como entorno de desarrollo, aunque no sea a nivel profesional. 
El proyecto consiste en un TabbedPage cuya página de inicio "Inicio y Tareas" es la administración de Tareas, cuya propiedad string
se ingresa en el editor, pudiendo agregar a esta, Fecha desde un DatePicker y hora desde un TimePicker, ambos en la interfaz 
de usuario. El estado de la Tarea se ingresa mediante un CheckBox, que seleccionado con ✓ equivale a Tarea en espera,
mientras que sin seleccionar equivale a Completado. Esta página incluye un Label con un TapGestureRecognizer que con un Tap
llama, en forma aleatoria, una Frase de la Base de Datos en la página "Portal de Frases", si aún no hay frase alguna en la base de datos, 
habra un aviso de que no hay frases disponibles, en tal caso, el usuario podra ir a la página Portal de Frases y visitar los sitios web en su parte inferior 
para poder agregar las frases de su preferencia.
## Captura de pantalla "Inicio y Tareas"

![Captura de pantalla Inicio y Tareas](/inicioYtareas.png)

## Captura de pantalla "Portal de Frases"
![Captura de pantalla "Portal de Frases"](/portalDfrases.png)
![ ](/portalDfrases2.png)

