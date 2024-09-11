# Star Wars API
Una API simple que simula un registro de habitantes del planeta Tatooine realizado por el Imperio Galáctico.
Los habitantes tienen los siguientes atributos:
- ID
- Nombre
- Planeta de origen
- Especie
- Es rebelde o no

Además, los planetas y especies también se registran con un ID y un nombre

## Endpoints
### Habitants
#### GET /Habitants
Devuelve una lista de todos los habitantes registrados

#### GET /Habitants/{id}
Devuelve un habitante en concreto

#### POST /Habitants
Crea un nuevo habitante

#### GET /Habitants/Rebels
Devuelve una lista de todos los habitantes rebeldes

### Planets
#### GET /Planets
Devuelve una lista de con los planetas registrados

### Species
#### GET /Species
Devuelve una lista de las especies registradas

## Usar la API en local
Para ejecutar esta API en tu equipo, hay instalar estas tecnologías:
- [Visual Studio](https://visualstudio.microsoft.com/es/downloads/)
- [.NET](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/es-es/sql-server/sql-server-downloads)
- [SQL Server Management Studio](https://docs.microsoft.com/es-es/sql/ssms/download-sql-server-management-studio-ssms)

Una vez instaladas, hay que clonar el repositorio.
Para configurar el servidor y la BBDD de SQL Server desde Management Studio si no lo hubiéramos hecho antes:
- Conectarnos a nuestro servidor desde Windows
- Click derecho en el servidor > Propiedades > Seguridad
- Marcar la opción de Modo de autenticación de SQL Server y Windows
- Damos a aceptar
- Click derecho en el servidor > Reiniciar
- Nos volvemos a conectar desde Windows
- En las carpetas del servidor > Seguridad > Inicios de sesión
- Click derecho en el que se llama **sa** > Propiedades
- Vamos a la pestaña de Estado y marcamos la opción de Conceder y la de Habilitada
- Vamos a la pestaña de General, cambiamos la contraseña y desmarcamos Exigir directivas de contraseña
- Damos a aceptar
- Si ahora nos intentamos conectar con el usuario **sa** y la contraseña que hemos puesto, debería funcionar
 
Luego tendremos dos opciones:
1. Creamos nuestra BBDD propia, ejecutamos el `script.sql` en la base de datos y modificamos el archivo `appsettings.json` con la cadena de conexión
2. Para no tocar la cadena de conexión de `appsettings.json`, creamos una nueva BBDD llamada **encamina** y ejecutamos el `script.sql` dentro