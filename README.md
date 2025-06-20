# Web Api Library

Api para el registro digital de documentos

---

## Prerrequisitos

* Se necesita tener .NET 8 SDK instalado en tu máquina.
* (Opcional) Docker instalado.

---

## Levantar la aplicación

Tienes dos opciones para levantar el proyecto:

### 1. Ejecución local

1. **Clona el repositorio**:

   ```bash
   git clone https://github.com/Cba01/api-rest-library.git
   cd api-rest-library
   ```

2. **Scripts de creación de BD**:

   * Dentro de la carpeta `db/scripts` encontrarás los archivos `.sql` necesarios para crear y poblar la base de datos.
   * Ejecuta esos scripts manualmente en tu gestor de base de datos (SQL Server / MySQL) antes de iniciar la API, en este orden:
     - library_documents.sql
     - library_indexentries.sql

3. **Configura conexion a base de datos**:
   
   Edita `appsettings.json` con tu conexión a BD y otros ajustes:

   ```dotenv
   "ConnectionStrings": {
      "Connection"="server=...;port=...;user=...;password=...;database=..."
   }
   ```

5. **Restaura y ejecuta**:

   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```

### 2. Ejecución con Docker

1. Asegúrate de tener Docker instalado.
   
2. **Configura conexion a base de datos**:
   
   Edita `appsettings.json` con tu conexión a BD y otros ajustes:

   ```dotenv
   "ConnectionStrings": {
      "Connection"="server=host.docker.internal;port=...;user=...;password=...;database=..."
   }
   ```
3. **Construir la imagen y Ejecutar el contenedor**:

   ```bash
   docker build -t mi-api .; docker run -d -p 5065:80 -e ASPNETCORE_URLS='http://+:80' -e ASPNETCORE_ENVIRONMENT='Development' --name mi-api-container mi-api
   ```



## Swagger / OpenAPI

Una vez en ejecución, accede a:

```
http://localhost:5065/swagger/index.html
```

para ver y probar todos los endpoints.

---

## Documentación de la API de Documentos

### Endpoints

| Método | Ruta                                    | Descripción                                        |
|:------:|:----------------------------------------|:---------------------------------------------------|
| GET    | `/api/documents/{id}`                  | Obtener un documento por su ID (incluye índice).   |
| GET    | `/api/documents`                       | Buscar documentos con filtros y paginación.        |
| POST   | `/api/documents`                       | Crear un nuevo documento con sus entradas de índice. |
| PUT    | `/api/documents/{id}`                  | Reemplazar completamente un documento existente.   |
| DELETE | `/api/documents/{id}`                  | Eliminar (soft-delete) un documento y sus índices. |

---

### Ejemplo: obtener documento por ID

```http
GET http://localhost:5065/api/documents/1

