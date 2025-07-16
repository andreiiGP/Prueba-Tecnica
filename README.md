Cordial saludo
Hago  entrega de la prueba tecnica, espero sea de su agrado.

1. para ingresar a el index de la interfaz web es por https://localhost:7029/index.html una vez ejecuten la solucion
2. para que funcione correctamnete nos toca abrir dos instancias de vs2022 una para que corra el API Y otra para que se ejecute el aplicativo de consola que organiza los PDF
3. script de la base de datos para que la puedan montar en SQL server


CREATE DATABASE PdfProcessor,
USE DATABASE PdfProcessor

CREATE TABLE DocKey (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Clave NVARCHAR(255) NOT NULL,
    DocName NVARCHAR(255) NOT NULL
);

CREATE TABLE LogProcesamiento (
    Id INT PRIMARY KEY IDENTITY(1,1),
    NombreArchivo NVARCHAR(255),
    Estado NVARCHAR(50), -- 'Processed' o 'Unknown'
    Fecha DATETIME DEFAULT GETDATE()
);

4. Espero su cordial respuesta.
