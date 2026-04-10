# Kaizen Modular

## Visión de arquitectura (monolito modular robusto)

Kaizen se diseña como **monolito modular por dominios**, con límites estrictos entre:

1. **Gastronomía** (operación productiva y comercial del rubro gastronómico).
2. **Gimnasio** (operación comercial, operativa y de servicio del rubro fitness).
3. **Administración** (núcleo contable-financiero, fiscal y de consolidación).

La regla central es: **la operación nace en Gastronomía/Gimnasio y el impacto administrativo se consolida en Administración**, sin duplicar lógica de negocio ni recargar manualmente datos ya conocidos por la operación.

---

## 1) Propuesta de arquitectura general del sistema Kaizen

### 1.1 Principios no negociables

- **Separación fuerte de dominios**: cada módulo tiene su propio modelo, procesos, UI y permisos.
- **Integración explícita por contratos internos**: no “acceso libre” entre modelos de apps.
- **Trazabilidad extremo a extremo**: todo impacto administrativo referencia el origen operativo.
- **Núcleo común mínimo y estable**: catálogo de personas/terceros, sucursales, monedas, impuestos, usuarios/roles, auditoría.
- **Preparado para API y app futura**: casos de uso encapsulados en servicios; vistas desacopladas de reglas.
- **Castellano por defecto**: nombres de apps, clases de dominio, campos, permisos, menús, etiquetas y documentación en castellano (salvo restricciones del framework).

### 1.2 Estructura técnica recomendada (Django monolito modular)

```text
kaizen/
  nucleo/
    personas/
    terceros/
    sedes/
    seguridad/
    auditoria/
    parametros/
    integraciones/
  gastronomia/
    catalogo/
    produccion/
    inventario/
    compras/
    ventas/
    caja/
    cocina/
    reportes/
  gimnasio/
    crm/
    socios/
    membresias/
    acceso/
    clases/
    salud/
    comercial/
    operacion/
    reportes/
  administracion/
    facturacion/
    contabilidad/
    tesoreria/
    cuentas_corrientes/
    impuestos/
    compras_admin/
    reportes/
    tableros/
  interfaces/
    web/
    api_interna/
    api_externa/
```

### 1.3 Patrón de implementación interno

- **Modelos por dominio** (cada app es dueña de sus tablas).
- **Servicios de aplicación** para casos de uso complejos.
- **Eventos de dominio internos** (tabla `evento_dominio` + despachador) para desacoplar impactos administrativos.
- **Proyecciones y vistas materializadas** para tableros premium de alto rendimiento.
- **Control transaccional estricto** en operaciones críticas (ventas, cierres, asientos, cobros/pagos).

---

## 2) División real de módulos y submódulos

### 2.1 Núcleo compartido (no funcional de negocio, sí transversal)

- **personas/terceros**: maestro único de personas físicas/jurídicas (cliente, socio, proveedor, empleado).
- **sedes**: sucursales, depósitos, puntos de venta y configuración por sede.
- **seguridad**: usuarios, grupos, roles, permisos granulares por acción.
- **auditoria**: bitácora inmutable de acciones críticas y cambios sensibles.
- **parametros**: listas configurables (impuestos, estados, medios de pago, etc.).
- **integraciones**: conectores externos desacoplados con cola/reintentos.

### 2.2 Gastronomía

- **catalogo**: productos gastronómicos, insumos, unidades, conversiones, listas de precios.
- **produccion**: recetas/fórmulas, lotes, órdenes de producción, rendimientos y costos estándar/real.
- **inventario**: stock por insumo y producto, movimientos, ajustes, mermas, transferencias.
- **compras**: proveedores, solicitudes, órdenes de compra, recepciones, costos de reposición.
- **ventas**: pedidos/comandas, estados, canales de venta, facturación operativa.
- **cocina**: cola de preparación, despacho, tiempos, alertas y trazabilidad por pedido.
- **caja**: apertura, turnos, arqueos, cierres, diferencias.
- **reportes**: margen, costo real, desperdicio, productividad, ventas por canal/franja.

### 2.3 Gimnasio

- **crm**: leads, embudo, origen, estado, tareas de seguimiento.
- **socios**: alta, legajo, contactos, emergencias, documentación, observaciones.
- **membresias**: planes, abonos, vigencias, renovaciones, congelamientos, becas/bonos, morosidad.
- **acceso**: check-in, reglas de acceso por pago y plan, bloqueos y alertas.
- **clases**: actividades, disciplinas, grilla, cupos, reservas, lista de espera, asistencia.
- **salud**: ficha base, evaluaciones, objetivos, mediciones, evolución.
- **comercial**: venta de planes/productos/adicionales, recordatorios, retención.
- **operacion**: staff, profesores, recepcionistas, turnos y preparación para comisiones futuras.
- **reportes**: churn, asistencia, ocupación, morosidad, conversión de leads.

### 2.4 Administración

- **facturacion**: comprobantes, CAE/afip (según evolución), notas de crédito/débito.
- **contabilidad**: plan de cuentas, asientos automáticos/manuales, cierres contables.
- **tesoreria**: caja y bancos, conciliaciones, flujo de fondos, pagos y cobranzas.
- **cuentas_corrientes**: saldos y movimientos de clientes/socios/proveedores.
- **impuestos**: liquidaciones, percepciones, retenciones, reportes fiscales.
- **compras_admin**: circuito administrativo de compras no operativas y aprobaciones.
- **reportes/tableros**: KPIs consolidados y por módulo/sede/canal.

---

## 3) Responsabilidades por módulo (límites estrictos)

- **Gastronomía** decide y ejecuta su operación (receta, producción, pedido, despacho, merma, etc.).
- **Gimnasio** decide y ejecuta su operación (alta socio, plan, acceso, reserva, seguimiento).
- **Administración** **no** define reglas operativas finas de Gastronomía/Gimnasio; recibe eventos validados, consolida y gobierna finanzas/contabilidad/fiscal.
- **Núcleo** brinda identidad, seguridad, sedes y trazabilidad, sin absorber lógica de negocio específica.

---

## 4) Modelo de integración entre Gastronomía, Gimnasio y Administración

### 4.1 Contrato de evento interno (ejemplos)

Cada evento crítico se registra con:

- `tipo_evento` (ej. `venta_gastronomia_confirmada`, `membresia_renovada`).
- `modulo_origen`.
- `entidad_origen_id`.
- `sede_id`.
- `fecha_operacion`.
- `payload` versionado.
- `estado_procesamiento` (pendiente, aplicado, error, compensado).

### 4.2 Flujos clave

1. **Venta gastronómica**
   - Gastronomía confirma pedido/cobro → descuenta stock/insumos según receta real.
   - Emite evento de venta + costo + movimiento de caja.
   - Administración genera: comprobante, movimiento en tesorería, asiento contable, cuenta corriente si aplica.

2. **Compra de insumos**
   - Gastronomía recibe mercadería → actualiza stock y costo.
   - Evento de recepción/factura proveedor.
   - Administración registra obligación de pago, impuesto crédito fiscal y asiento.

3. **Renovación de membresía**
   - Gimnasio valida plan/promoción → registra vigencia y cobro.
   - Evento de cobro y devengamiento.
   - Administración impacta en caja/banco, facturación y cuentas corrientes.

4. **Acceso bloqueado por morosidad**
   - Regla en Gimnasio determina bloqueo.
   - Administración solo consume para métricas/riesgo/cobranzas, no para decidir lógica de acceso.

### 4.3 Reglas de consistencia

- Idempotencia por `id_evento` para evitar doble impacto.
- Reintentos automáticos con cola de errores.
- Compensaciones explícitas (anulación/reverso), nunca borrado destructivo.
- Versionado de contratos para evolución sin romper integraciones.

---

## 5) Entidades principales (modelo de alto nivel)

### 5.1 Núcleo

- `Persona`, `Tercero`, `RolTercero`
- `Sede`, `Deposito`, `PuntoOperacion`
- `Usuario`, `Rol`, `Permiso`, `AsignacionRol`
- `RegistroAuditoria`, `EventoDominio`, `ParametroSistema`

### 5.2 Gastronomía

- `ProductoGastronomico`, `Insumo`, `Receta`, `RecetaItem`
- `OrdenProduccion`, `ConsumoProduccion`, `MermaProduccion`
- `Existencia`, `MovimientoInventario`, `AjusteInventario`
- `ProveedorGastro`, `OrdenCompraGastro`, `RecepcionCompraGastro`
- `PedidoGastro`, `LineaPedidoGastro`, `EstadoPedido`, `Comanda`
- `CajaGastro`, `TurnoCajaGastro`, `MovimientoCajaGastro`, `CierreCajaGastro`

### 5.3 Gimnasio

- `Lead`, `EstadoLead`, `InteraccionLead`
- `Socio`, `LegajoSocio`, `ContactoSocio`, `DocumentoSocio`
- `Plan`, `Membresia`, `CongelamientoMembresia`, `BeneficioMembresia`
- `ControlAcceso`, `IntentoAcceso`, `ReglaAcceso`
- `Actividad`, `ClaseProgramada`, `ReservaClase`, `AsistenciaClase`
- `EvaluacionFisica`, `ObjetivoSocio`, `MedicionCorporal`, `SeguimientoEntrenamiento`
- `VentaGym`, `ItemVentaGym`, `CampaniaRetencion`

### 5.4 Administración

- `Comprobante`, `ItemComprobante`, `CondicionFiscal`
- `CuentaContable`, `AsientoContable`, `MovimientoContable`
- `CuentaCorriente`, `MovimientoCuentaCorriente`
- `CajaAdministrativa`, `MovimientoTesoreria`, `ConciliacionBancaria`
- `ObligacionImpositiva`, `LiquidacionImpositiva`
- `Indicador`, `Tablero`, `CorteConsolidado`

---

## 6) Propuesta de permisos y roles

### 6.1 Matriz base (RBAC + alcance por sede)

- **Superadministración**: configuración global, seguridad, auditoría, reportes consolidados.
- **Administración central**: facturación, contabilidad, tesorería, impuestos, tableros globales.
- **Gerencia gastronómica**: producción, compras gastro, costos, reportes gastro.
- **Operación gastronómica**: pedidos, cocina, caja, inventario operativo.
- **Gerencia gimnasio**: planes, staff, reportes gym, políticas comerciales gym.
- **Operación gimnasio**: admisión, reservas, check-in, asistencia, ventas de mostrador.
- **Cobranzas**: cuentas corrientes, seguimiento de deuda, acuerdos de pago.
- **Auditoría**: solo lectura amplia + exportes trazables.

### 6.2 Permisos críticos por acción

- `puede_confirmar_venta_gastro`
- `puede_cerrar_caja_gastro`
- `puede_registrar_merma`
- `puede_aprobar_compra`
- `puede_habilitar_acceso_moroso`
- `puede_modificar_plan_activo`
- `puede_emitir_comprobante`
- `puede_publicar_asiento`
- `puede_revertir_operacion_critica`

Todos con:
- alcance por sede,
- separación entre crear/aprobar/anular,
- doble control para acciones sensibles.

---

## 7) Orden correcto de implementación (por etapas)

### Etapa 0 — Diagnóstico y refactor estructural

- Inventario de apps actuales y deuda técnica.
- Definición de fronteras de dominio.
- Renombre/migración de módulos ambiguos.

### Etapa 1 — Núcleo transversal sólido

- Maestro de personas/terceros.
- Seguridad (roles/permisos por sede).
- Auditoría central.
- Parámetros configurables.

### Etapa 2 — Motor de integración interno

- Tabla de eventos de dominio.
- Despachador + idempotencia + reintentos + compensaciones.
- Trazabilidad cruzada origen→impacto administrativo.

### Etapa 3 — Gastronomía operativo completo (MVP robusto real)

- Catálogo + recetas + producción + inventario + compras + ventas + caja + cocina.
- Impacto automático en Administración para venta/compra/caja/stock.

### Etapa 4 — Administración base productiva

- Facturación + tesorería + cuentas corrientes + contabilidad automática básica.
- Reportes consolidados por módulo/sede/canal.

### Etapa 5 — Gimnasio operativo completo

- CRM + socios + membresías + acceso + clases + comercial + salud base.
- Integración completa con Administración.

### Etapa 6 — Front premium y APIs

- Sistema de diseño unificado, navegación por contexto operativo.
- Tableros avanzados con métricas en tiempo real.
- API externa segura para app futura de gimnasio y conectores externos.

### Etapa 7 — Optimización y escalabilidad

- Caches selectivos, partición por fechas/sede en tablas de alto volumen.
- Observabilidad, monitoreo de SLA de procesos críticos.

---

## 8) Riesgos y errores comunes a evitar

1. **Administración como “bolsa de todo”** → rompe dominio y mantenimiento.
2. **Duplicar maestros de personas/clientes/socios** → inconsistencias masivas.
3. **Acoplar lógica con templates** → imposible exponer APIs limpias luego.
4. **No diseñar idempotencia/compensación** → dobles asientos o saldos corruptos.
5. **Permisos demasiado amplios** → riesgo operativo y de fraude.
6. **Estados ambiguos en pedidos/membresías** → caos de operación.
7. **Hardcode de reglas comerciales** → baja adaptabilidad del negocio.
8. **UI simple sin jerarquía operacional** → bajo rendimiento del equipo.

---

## Lineamientos para un front premium y avanzado

- **Diseño por contexto operativo** (no menú plano): vistas para caja, cocina, recepción, administración, gerencia.
- **Tableros accionables** con KPIs + alertas + drill-down por sede/canal.
- **Flujos asistidos** (wizards) para altas complejas y cierres.
- **Estados visuales robustos** (semáforos, SLA, bloqueos, excepciones).
- **Componentes reutilizables**: tablas avanzadas, filtros persistentes, auditoría contextual.
- **Rendimiento UX**: paginación server-side, búsquedas rápidas, atajos de teclado para operación.
- **Accesibilidad y consistencia**: diseño profesional, responsive real, feedback inmediato.

---

## Criterio de reorganización recomendado

Si la base actual no soporta estos límites, se recomienda refactorizar de forma controlada:

- extraer lógica transversal al `nucleo`,
- reducir dependencias cruzadas directas,
- mover reglas de negocio a servicios de aplicación,
- formalizar contratos de integración por eventos,
- renombrar componentes para claridad semántica en castellano.

Esta estrategia prioriza **hacer bien**: base sólida, mantenible y lista para crecimiento multi-sede, multi-canal y futura app.
