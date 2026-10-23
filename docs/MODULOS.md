# Módulos y criterios de aceptación

Estado general: **inventario por verificar contra la instalación actual**.

| Módulo | Verificaciones requeridas |
| --- | --- |
| Catálogo | Un producto único por referencia vigente; precios/categorías/SKU correctos |
| Recetas | Versiones vigentes, unidades y rendimientos; pendientes explícitos |
| Ingredientes | Conversión g↔kg, ml↔lt; insumos equivalentes sin duplicidad |
| Cocina | Preparación, extras, observaciones, cambio de estados |
| Pedidos | Venta completa, modificación, reintento, cancelación |
| Stock | Descuento único y verificable con auditoría y reversión |
| Combinados | Prorrateo por número de piezas; nigiris por unidad |
| Poke a medida | Sustitución de bases y toppings sin doble descuento |
| Caja | Precios, adicionales, delivery y pagos |
| Integraciones | SKU externos, modificadores, sincronización |
| Administración | Reportes de costo/margen e historial |

Las validaciones concretas deben convertirse en tests cuando se identifique el código y la base actual.
