# Web Api Library

Api para el registro digital de documentos

---

## Prerrequisitos

* Se necesita tener .NET 8 SDK instalado en tu máquina.
* (Opcional) Docker instalado.

---

## 📂 Estructura del repositorio

```bash
├── src/
│   └── TuApiProyecto.csproj
├── Dockerfile
├── .dockerignore
└── README.md
```

---

## ⚙️ Configuración de variables de entorno

1. Copia el archivo de ejemplo:

   ```bash
   cp .env.example .env
   ```
2. Abre `.env` y define tus variables:

   ```dotenv
   ConnectionStrings__DefaultConnection="Server=...;Database=...;User Id=...;Password=..."
   ASPNETCORE_ENVIRONMENT=Development
   ```

---

## 🚀 Levantar la aplicación

### 1. Ejecución local

1. **Clona el repositorio**:

   ```bash
   git clone https://github.com/Cba01/api-rest-library.git
   cd api-rest-library
   ```

2. **Scripts de creación de BD**:

   * Dentro de la carpeta `db/scripts` encontrarás los archivos `.sql` necesarios para crear y poblar la base de datos.
   * Ejecuta esos scripts manualmente en tu gestor de base de datos (SQL Server / MySQL) antes de iniciar la API, en este orden:
     1.-library_documents.sql
     2.-library_indexentries.sql

3. **Configura variables de entorno**:

   ```bash
   cp .env.example .env
   ```

   Edita `.env` con tu conexión a BD y otros ajustes:

   ```dotenv
   ConnectionStrings__DefaultConnection="Server=localhost;Database=MiBaseDeDatos;User Id=...;Password=..."
   ASPNETCORE_ENVIRONMENT=Development
   ```

4. **Restaura y ejecuta**:

   ```bash
   dotnet restore
   dotnet build
   dotnet run --project src/TuApiProyecto.csproj
   ```

La API estará en:

```
https://localhost:5001
http://localhost:5000
```

### 2. Ejecución con Docker

1. Asegúrate de tener Docker instalado.
2. **Construir la imagen**:

   ```bash
   docker build -t mi-api:latest .
   ```
3. **Ejecutar el contenedor**:

   ```bash
   docker run -d -p 80:80 --env-file .env --name mi-api mi-api:latest
   ```

Luego abre `http://localhost` en tu navegador.


## 📖 Swagger / OpenAPI

Una vez en ejecución, accede a:

```
http://localhost/swagger/index.html
```

para ver y probar todos los endpoints.

---

