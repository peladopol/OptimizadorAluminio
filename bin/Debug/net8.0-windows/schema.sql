-- Perfiles de aluminio
CREATE TABLE IF NOT EXISTS Perfil (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Codigo TEXT NOT NULL UNIQUE,
    Descripcion TEXT NOT NULL,
    PesoKgMetro REAL NOT NULL,
    LargoBarraMM INTEGER NOT NULL DEFAULT 6150,
    LargoUtilMM INTEGER NOT NULL DEFAULT 6100,
    CorteExtremoMM INTEGER NOT NULL DEFAULT 1
);

-- Configuración de precios
CREATE TABLE IF NOT EXISTS ConfiguracionPrecio (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    PrecioKg REAL NOT NULL,
    Moneda TEXT NOT NULL, -- 'USD' o 'ARS'
    CotizacionUSD REAL NOT NULL,
    Fecha TEXT NOT NULL
);

-- Presupuestos
CREATE TABLE IF NOT EXISTS Presupuesto (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Numero INTEGER NOT NULL UNIQUE,
    Fecha TEXT NOT NULL,
    Descripcion TEXT
);

-- Ítems del presupuesto (cortes)
CREATE TABLE IF NOT EXISTS PresupuestoItem (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    PresupuestoId INTEGER NOT NULL,
    CodigoPerfil TEXT NOT NULL,
    LargoMM INTEGER NOT NULL,
    Ventana TEXT NOT NULL,
    Angulo INTEGER NOT NULL, -- 45 o 90
    FOREIGN KEY (PresupuestoId) REFERENCES Presupuesto(Id)
);

-- Desperdicios
CREATE TABLE IF NOT EXISTS Desperdicio (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    PresupuestoId INTEGER NOT NULL,
    CodigoPerfil TEXT NOT NULL,
    LargoSobranteMM INTEGER NOT NULL
);
