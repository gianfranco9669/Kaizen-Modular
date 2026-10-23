# Migración de recetario REN — plan previo

**Estado: SIN EJECUTAR.** No se han alterado productos, recetas ni stock del sistema real.

## Fuentes

1. Relevamiento presencial de cocina del 23/10/2026: fuente prioritaria para gramajes y componentes.
2. Menú comercial REN: aporta productos, presentaciones, coberturas, complementos y precios.
3. Archivo integrado y manifest JSON de auditoría: materiales de trabajo **no publicados aquí** mientras el repositorio sea público.

## Reglas validadas

- Rolls de 8 piezas: 120 g de arroz **preparado** por roll; proteína estándar 60 g donde corresponda.
- Queso crema Finlandia y palta: 30 g **solo cuando son ingredientes del roll**, salvo excepciones.
- Buenos Aires clásico: salmón y palta. New York clásico: salmón y queso crema. Uramaki Buenos Aires premium es otro producto.
- Uramaki Masarikado: 124 g totales de salmón, sumando relleno y cobertura.
- Acompañamientos: tazitas de salsa de 40 g que se entregan **por pedido**, no por roll; wasabi 10 g por porción.
- Envío: $3.000, concepto comercial, no consumo de insumo.
- En los combinados, usar proporción 5/8 o 4/8 por roll, nigiri y sashimi por unidad.

## Bloqueantes de exactitud

- Faltan pesos de algunos ingredientes y de subpreparaciones, por ejemplo rebozados, salsas y rendimiento de arroz preparado.
- Confirmar la composición futura de Nigiri Mix y las diferencias del Poke Veggie.
- Confirmar si salsa de ostras del Harumaki Veggie es realmente vegetal.
- No establecer en cero silenciosamente ingredientes sin cantidad.

## Proceso previsto

1. Exportación actual de solo lectura.
2. Respaldo probado, aislamiento de datos y mapeo de IDs/SKU.
3. Comparativa producto → receta → insumo → stock.
4. Migración versionada e idempotente en copia de pruebas.
5. Tests de venta, combinados, tazitas por pedido y cancelación.
6. Aplicación controlada y verificación posmigración.
7. Actualización documental y anotación del despliegue en CHANGELOG.

Nunca borrar historial de pedidos ni reutilizar un SKU que refiera a otro producto.
