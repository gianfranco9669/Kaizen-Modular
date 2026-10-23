# Cómo recopilar el código actual en Windows

**Objetivo:** entregar para revisión una copia del código que existe en la computadora, sin modificar el sistema.

## Ruta corta: script corregido (23/10/2026)

1. Descargar [el recolector corregido](../scripts/REN_RECOLECTAR_CODIGO_SOLO_LECTURA.ps1) desde GitHub o el adjunto `REN_RECOLECTAR_CODIGO_SOLO_LECTURA_CORREGIDO.ps1` de la conversación.
2. Copiar el archivo `.ps1` a `C:\Users\Gianfranco\Desktop\Kaizen Modular` (si el proyecto sigue en esa ruta).
3. En el Explorador de Windows, abrir esa carpeta. Escribir `powershell` en la barra de direcciones y pulsar Enter.
4. Si descargó el adjunto corregido, ejecutar:

```powershell
powershell -ExecutionPolicy Bypass -File ".\REN_RECOLECTAR_CODIGO_SOLO_LECTURA_CORREGIDO.ps1"
```

5. Si descargó la versión de GitHub, ejecutar:

```powershell
powershell -ExecutionPolicy Bypass -File ".\REN_RECOLECTAR_CODIGO_SOLO_LECTURA.ps1"
```

6. Si la carpeta del proyecto está en otra ruta, añadir `-ProjectPath "D:\Ruta\Real\Kaizen Modular"` al comando.
7. Revisar el nuevo ZIP del Escritorio (`REN_Codigo_Actual_AAAAMMDD_HHMMSS.zip`) para verificar que no contenga claves, tokens, datos personales o contenido confidencial y enviarlo **privadamente** para la auditoría. Nunca subirlo a este repositorio público sin inspeccionarlo.

### Error corregido

La primera versión arrojaba: `No se encuentra la propiedad 'Count' en este objeto` en el filtro `Where-Object` si solo coincidía un elemento. La versión corregida usa una colección forzada y no cambia archivos del proyecto.

**No ejecutar todavía el SQL de auditoría** sin verificar base y permisos de lectura.

> El filtrado automático de nombres de archivo no garantiza que el código fuente esté libre de secretos escritos dentro de archivos permitidos.
