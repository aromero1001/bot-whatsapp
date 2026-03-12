# NurseBot API 🩺💬

Bienvenido a la **NurseBot API**, una solución independiente construida con **.NET 8/9 Minimal API** diseñada para automatizar el triaje inicial y la cotización de servicios de enfermería a domicilio.

## 🚀 Características Principales

- **Triaje Inteligente**: Identificación automática de servicios mediante palabras clave (`curas`, `inyectables`, `signos vitales`, `post-operatorio`).
- **Cotización Automatizada**: Precios estimados instantáneos para servicios comunes.
- **IA Integrada**: Soporte para respuestas naturales utilizando la abstracción `Microsoft.Extensions.AI`.
- **Documentación Interactiva**: OpenAPI (Swagger) integrado para pruebas fáciles.
- **Listo para la Nube**: Configurado para despliegue inmediato en Railway mediante Docker.

## 🛠️ Tecnologías Usadas

- **C# / .NET 8** (Minimal APIs)
- **Microsoft.Extensions.AI** (Abstracción de IA)
- **Swashbuckle.AspNetCore** (Swagger/OpenAPI)
- **Docker** (Multi-stage build)

## 📖 Cómo empezar

### Requisitos previos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/) (opcional, para despliegue)

### Ejecución local
1. Clona el repositorio o descarga los archivos.
2. Navega a la carpeta del proyecto.
3. Ejecuta el comando:
   ```powershell
   dotnet run
   ```
4. Accede a la documentación interactiva en: `http://localhost:8080/swagger`

## 📡 Endpoints del API

| Método | Endpoint | Descripción |
| :--- | :--- | :--- |
| `GET` | `/` | Health Check (Verifica que la API está online) |
| `POST` | `/chat` | Procesa mensajes y devuelve respuestas del bot |
| `GET` | `/swagger` | Interfaz de Swagger para pruebas |

### Ejemplo de petición (POST /chat)
```json
{
  "message": "Hola, necesito ponerle un inyectable a mi abuelo",
  "userId": "550e8400-e29b-41d4-a716-446655440000"
}
```

## ☁️ Despliegue en Railway

Este proyecto incluye un `Dockerfile` y `railway.json` listos para usar.
1. Sube tu código a GitHub.
2. En Railway, crea un nuevo proyecto desde tu repo.
3. Configura la variable `OPEN_AI_API_KEY` si deseas habilitar la IA avanzada.

## ⚖️ Principios de Diseño
Este proyecto sigue los principios **SOLID**, **KISS** y **YAGNI**, asegurando un código limpio, mantenible y sin complejidades innecesarias.
