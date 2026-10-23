# Cómo recopilar el código actual en Windows

**Objetivo:** entregar para revisión una copia del código que existe en la computadora, sin modificar el sistema.

1. Descargar `REN_Paquete_Auditoria_Migracion_v03.zip` desde la conversación con ChatGPT.
2. En el Explorador de Windows: clic derecho → **Extraer todo**.
3. Comprobar que el proyecto de Kaizen esté en `C:\\Users\\Gianfranco\\Desktop\\Kaizen Modular` (o anotar su ubicación real).
4. Abrir la carpeta descomprimida, escribir `powershell` en la barra de direcciones y pulsar Enter.
5. Ejecutar (copiar el comando completo):

```powershell
powershell -ExecutionPolicy Bypass -File ".\REN_RECOLECTAR_CODIGO_SOLO_LECTURA.ps1"
```

6. Si el proyecto no está en la ruta predeterminada, indicar la ruta real:

```powershell
powershell -ExecutionPolicy Bypass -File ".\REN_RECOLECTAR_CODIGO_SOLO_LECTURA.ps1" -ProjectPath "D:\Ruta\Real\Kaizen Modular"
```

7. En el Escritorio aparece `REN_Codigo_Actual_AAAAMMDD_HHMMSS.zip`.
8. Antes de compartirlo, revisar que no haya claves, tokens, datos personales o contenido empresarial confidencial dentro de archivos de código. Enviarlo directamente a la conversación de auditoría, **no al repositorio público**.

**El otro archivo SQL no se ejecuta todavía.** Se usará más adelante en una copia/usuario de solo lectura, después de verificar conexión y tablas.

Si PowerShell da un error de ruta, copiar el mensaje de error y verificar la carpeta principal. No mover ni borrar archivos del proyecto original.

> La exclusión automática de `.env` y otros nombres sensibles no detecta secretos escritos directamente dentro de archivos de código.
