# Módulos inspeccionados y criterios de aceptación

Revisión estática del código local del 23/10/2026. Ningún ítem se considera probado en ejecución.

| Módulo | Funcionalidad presente | Necesidad antes del recetario REN |
| --- | --- | --- |
| Catálogo | Productos, categorías y SKU | Conciliar nuevo menú, precios e IDs sin romper historial |
| Recetas | Guardar/listar receta por producto | Versiones históricas; receta compuesta; controlar incompletos |
| Insumos | Unidad, costo, stock y mínimos | Normalizar unidades, alias, ingredientes faltantes |
| Stock | Movimientos, semáforo, stock flexible | Elaboraciones/lotes, consumos de salsas por pedido |
| Pedidos | Idempotencia, estados, ampliaciones | Combinados y personalizaciones con cantidades efectivas |
| Cocina/KDS | Estados y rondas | Probar cambios de comanda, parcialidades y nuevas recetas |
| Caja | Apertura, cobros, movimientos y cierres | Precio delivery, reporte por canal y reconciliación |
| Administración | Compras, gastos, tesorería, comprobantes y reportes | Historizar costos y precios |
| Rappi | SKU, modificadores, importación y sincronización | Simular antes de publicar catálogo actualizado |
| Gimnasio | Entidades, controllers y pantallas propias | Queda fuera de esta migración |
| Seguridad | Cookies, permisos, CSRF y rate limiting | Revisar configuración de despliegue y secretos |

## Pruebas gastronómicas requeridas

- New York y Buenos Aires con ingredientes corregidos; cada roll 8 piezas y 120 g de arroz preparado.
- Combinados: cantidades de 4/8 o 5/8 por roll; nigiris/sashimi por pieza.
- Tazita de 40 g a nivel de pedido; dos rolls no multiplican automáticamente la salsa.
- Arroz cocido/preparado: lotes y rendimiento real sin doble consumo.
- Poke configurable: elección y reemplazo de bases y toppings; evitar doble descuento.
- Masarikado: 124 g **totales** de salmón; no duplicar cobertura.
- Pedido, ampliación, cancelación e importación Rappi idempotentes.
- Stock no inicializado y stock negativo: controles visibles.
