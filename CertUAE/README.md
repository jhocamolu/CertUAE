Manual de Usuario: CertUAE

Este manual proporciona instrucciones detalladas para usar la aplicación CertUAE, una herramienta diseñada para escanear directorios, analizar archivos PDF y TIFF, extraer metadatos y generar informes, así como generar un diccionario de datos de una base de datos MySQL.

1. Requisitos del Sistema

Antes de ejecutar la aplicación, asegurate de cumplir con los siguientes requisitos:

Sistema Operativo: Windows 10/11.

.NET SDK: .NET 8.0 o superior instalado.

Base de Datos (opcional): Acceso a una base de datos MySQL si querés usar la funcionalidad de generación de diccionario de datos.

Versión portable:

Descargar e instalar para las versiones X64 y 86X .NET Desktop Runtime 8.0.18 (x64 | x86).

2. Ejecución de la Aplicación

Para ejecutar la aplicación, abrír una terminal y navegá hasta el directorio donde se encuentra el archivo ejecutable o si esta en el directorio ejecutar la aplicación como se visualiza a continuacion.

CertUAE.exe

3. Menú Principal

Una vez que la aplicación se inicia, te presentará el siguiente menú principal:

--- Selecciona una opción ---

1. Escanear archivos y generar reportes.

2. Generar diccionario de datos de la base de datos.

Tu opción:

Ingresa el número correspondiente a la opción que deseas realizar y presione Enter.

4. Opciones del Programa

4.1. Escanear Archivos y Generar Reportes (Opción 1)

Esta opción te permite escanear un directorio especifico en busca de archivos PDF y TIFF, extraer sus metadatos y generar informes detallados en formato CSV.

Al seleccionar esta opción, se te pedirá que ingreses la ruta del directorio a escanear:

Por favor, introduce la ruta del directorio a escanear:

Ejemplo de entrada: C:\MisDocumentos\SNR o toda la unidad D:\

Después de ingresar la ruta, la aplicación comenzará a escanear y te presentará un submenú:

--- Selecciona una opción ---

1. Generar solo listado de archivos

2. Generar solo procesamiento.

3. Generar Certificación todos los archivos

Tu opción:

1. Generar solo listado de archivos: Esta opción creará un archivo CSV llamado ListadoArchivos.csv en una subcarpeta Cert-SNR dentro del directorio escaneado. Este archivo contendrá una lista de todos los archivos encontrados en el directorio y sus subdirectorios.

2. Generar solo procesamiento: Esta opción procesará los archivos PDF y TIFF encontrados, extrayendo metadatos como el número de páginas, tamaño, autor, título, etc. Luego, generará dos archivos CSV: pdf_report.csv y tiff_report.csv, junto con un informe general Cert.csv, todos ubicados en la subcarpeta Cert-SNR.

3. Generar Certificacion todos los archivos: Esta opción ejecutará ambas funciones: primero generará el listado de archivos y luego realizará el procesamiento y la generación de reportes detallados.

Archivos de Salida (en la carpeta [DirectorioEscaneado]\Cert-SNR):

ListadoArchivos.csv: Contiene la ruta completa de todos los archivos encontrados en el directorio y sus subdirectorios.

pdf_report.csv: Un informe CSV con los siguientes campos para cada archivo PDF:

Nombre: Nombre del archivo PDF.

Ruta: Ruta completa del archivo PDF.

TamanoBytes: Tamaño del archivo en bytes.

Paginas: Número de páginas del PDF.

CantidadTiffs: Número de archivos TIFF encontrados en el mismo directorio (útil para comparación).

ContieneXml: Indica "Si" o "No" si el directorio contiene archivos XML/XMP.

DiferenciaTiffsVsPaginas: Diferencia entre el número de páginas del PDF y la cantidad de TIFFs.

PdfAuthor: Autor del PDF (metadato).

PdfTitle: Título del PDF (metadato).

PdfSubject: Asunto del PDF (metadato).

PdfCreator: Programa que creó el PDF (metadato).

PdfProducer: Programa que produjo el PDF (metadato).

PdfHashType: Tipo de hash del archivo.

PdfHash: Hash del archivo.

PdfCreationDate: Fecha de creación del PDF.

PdfModificationDate: Fecha de última modificación del PDF.

PdfDescription: Palabras clave/descripción del PDF (metadato).

tiff_report.csv: Un informe CSV con los siguientes campos para cada archivo TIFF:

Nombre: Nombre del archivo TIFF.

Ruta: Ruta completa del archivo TIFF.

TamanoBytes: Tamaño del archivo en bytes.

Cert.csv: Un informe general que resume el total de PDFs, TIFFs, XML/XMPs encontrados, el total de bytes procesados y las fechas de inicio y fin del escaneo.

4.2. Generar Diccionario de Datos de la Base de Datos (Opción 2)

Esta opción generará un diccionario de datos de tu base de datos MySQL, extrayendo información sobre las tablas y columnas. Los resultados se guardarán en un archivo CSV.

Al seleccionar esta opción, se te pedirá que ingreses la ruta donde querés guardar el diccionario de datos:

Introduce la ruta para guardar el diccionario de datos (ej: C:\temp\reports):

Ejemplo de entrada: C:\InformesDB

La aplicación creará el directorio si no existe y luego generará el archivo CSV con el diccionario de datos en la ubicación especificada.

5. Salir del Programa

Después de ejecutar una de las opciones, la aplicación te preguntará si deseás regresar al menú principal (0) o finalizar (9).

Escaneo directorio finalizo: 2025-07-24 14:12

0. Regresar Menu.

9. Finalizar.

Ingresá 0 para volver al menú principal y seleccionar otra opción.

Ingresá 9 para salir de la aplicación.

6. Mensajes de Error Comunes

"La cadena de conexión no fue proporcionada o es inválida. La aplicación no puede continuar.": Asegurate de que tu archivo appsettings.json exista y contenga una cadena de conexión DefaultConnection válida bajo la sección ConnectionStrings.

"Error: La ruta '...' no es un directorio válido o está vacía.": Verificá que la ruta del directorio que ingresaste para escanear sea correcta y exista.

"⚠ No se pudo leer imagen TIFF: ... - Error: ...": Puede haber un problema con el archivo TIFF específico (corrupción, formato no compatible) o permisos de acceso.