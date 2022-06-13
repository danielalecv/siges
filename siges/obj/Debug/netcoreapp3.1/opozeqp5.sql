IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [ActivoFijo] (
    [Id] int NOT NULL IDENTITY,
    [Descripcion] nvarchar(max) NULL,
    [Clave] nvarchar(max) NULL,
    [Marca] nvarchar(max) NULL,
    [Tipo] nvarchar(max) NULL,
    [FechaFactura] datetime2 NOT NULL,
    [FechaAlta] datetime2 NOT NULL,
    [Precio] decimal(18, 2) NOT NULL,
    [Estatus] bit NOT NULL,
    [Opcional1] nvarchar(max) NULL,
    [Opcional2] nvarchar(max) NULL,
    CONSTRAINT [PK_ActivoFijo] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Administracion] (
    [Id] int NOT NULL IDENTITY,
    [Estatus] bit NOT NULL,
    [OrdenServicioId] int NOT NULL,
    [PersonaId] int NOT NULL,
    [FechaAdministrativa] datetime2 NOT NULL,
    [FacturaFolio] nvarchar(max) NULL,
    [FacturaFecha] datetime2 NOT NULL,
    CONSTRAINT [PK_Administracion] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Bitacora] (
    [ID] int NOT NULL IDENTITY,
    [UserId] nvarchar(max) NULL,
    [EventDate] datetime2 NOT NULL,
    [Event] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [Section] nvarchar(max) NULL,
    CONSTRAINT [PK_Bitacora] PRIMARY KEY ([ID])
);

GO

CREATE TABLE [Cliente] (
    [Id] int NOT NULL IDENTITY,
    [RazonSocial] nvarchar(max) NULL,
    [RFC] nvarchar(max) NULL,
    [Telefono] nvarchar(max) NULL,
    [Opcional1] nvarchar(max) NULL,
    [Opcional2] nvarchar(max) NULL,
    [Estatus] bit NOT NULL,
    CONSTRAINT [PK_Cliente] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Comercial] (
    [Id] int NOT NULL IDENTITY,
    [Estatus] bit NOT NULL,
    [OrdenServicioId] int NOT NULL,
    [PersonaId] int NOT NULL,
    [FechaAdministrativa] datetime2 NOT NULL,
    CONSTRAINT [PK_Comercial] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Direccion] (
    [Id] int NOT NULL IDENTITY,
    [calle] nvarchar(max) NULL,
    [numero] int NOT NULL,
    [colonia] nvarchar(max) NULL,
    [cp] int NOT NULL,
    [municipio] nvarchar(max) NULL,
    [entidadFederativa] nvarchar(max) NULL,
    [estatus] bit NOT NULL,
    CONSTRAINT [PK_Direccion] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Insumo] (
    [Id] int NOT NULL IDENTITY,
    [Descripcion] nvarchar(max) NULL,
    [Clave] nvarchar(max) NULL,
    [Marca] nvarchar(max) NULL,
    [Tipo] nvarchar(max) NULL,
    [Precio] decimal(18, 2) NOT NULL,
    [Estatus] bit NOT NULL,
    [Opcional1] nvarchar(max) NULL,
    [Opcional2] nvarchar(max) NULL,
    CONSTRAINT [PK_Insumo] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [LineaNegocio] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NULL,
    [Estatus] bit NOT NULL,
    [Opcional1] nvarchar(max) NULL,
    [Opcional2] nvarchar(max) NULL,
    CONSTRAINT [PK_LineaNegocio] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Producto] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NULL,
    [Marca] nvarchar(max) NULL,
    [FechaAlta] datetime2 NOT NULL,
    [Estatus] bit NOT NULL,
    [Opcional1] nvarchar(max) NULL,
    [Opcional2] nvarchar(max) NULL,
    CONSTRAINT [PK_Producto] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Settings] (
    [Id] int NOT NULL IDENTITY,
    [Version] nvarchar(max) NULL,
    [FolioPrefix] nvarchar(max) NULL,
    [FolioDigitsLong] int NOT NULL,
    [EmailHost] nvarchar(max) NULL,
    [EmailPort] nvarchar(max) NULL,
    [EmailUser] nvarchar(max) NULL,
    [EmailPass] nvarchar(max) NULL,
    [RemainingDaysToUpload] int NOT NULL,
    [EmailEnableSSL] bit NOT NULL,
    [MaxCaractersFields] int NOT NULL,
    [ValidateMinimumDate] bit NOT NULL,
    [MinimumDateCriteria] nvarchar(max) NULL,
    [AttachmentFile1] varbinary(max) NULL,
    [AttachmentFile1Name] nvarchar(max) NULL,
    [SendAttachmentFile] bit NOT NULL,
    CONSTRAINT [PK_Settings] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Ubicacion] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NULL,
    [Direccion] nvarchar(max) NULL,
    [Contacto] nvarchar(max) NULL,
    [ContactoTelefono] nvarchar(max) NULL,
    [ContactoEmail] nvarchar(max) NULL,
    [ContactoOpcional] nvarchar(max) NULL,
    [ContactoOpcionalTelefono] nvarchar(max) NULL,
    [ContactoOpcionalEmail] nvarchar(max) NULL,
    [Estatus] bit NOT NULL,
    [ClienteId] int NOT NULL,
    CONSTRAINT [PK_Ubicacion] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Ubicacion_Cliente_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Cliente] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Persona] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NULL,
    [Paterno] nvarchar(max) NULL,
    [Materno] nvarchar(max) NULL,
    [RFC] nvarchar(max) NULL,
    [CURP] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [DirId] int NULL,
    [ClaveEmpleado] nvarchar(max) NULL,
    [Telefono] nvarchar(max) NULL,
    [TelefonoContacto] nvarchar(max) NULL,
    [Estatus] bit NOT NULL,
    [Categoria] nvarchar(max) NULL,
    [Puesto] nvarchar(max) NULL,
    [Direccion] nvarchar(max) NULL,
    [EntidadFederativa] nvarchar(max) NULL,
    [Municipio] nvarchar(max) NULL,
    [Sueldo] decimal(18, 2) NOT NULL,
    [Opcional1] nvarchar(max) NULL,
    [Opcional2] nvarchar(max) NULL,
    [Fotografia] varbinary(max) NULL,
    [UsuarioId] int NOT NULL,
    [PerfilId] int NOT NULL,
    CONSTRAINT [PK_Persona] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Persona_Direccion_DirId] FOREIGN KEY ([DirId]) REFERENCES [Direccion] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Servicio] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NULL,
    [Descripcion] nvarchar(max) NULL,
    [LineaNegocioId] int NULL,
    [Estatus] bit NOT NULL,
    [Opcional1] nvarchar(max) NULL,
    [Opcional2] nvarchar(max) NULL,
    CONSTRAINT [PK_Servicio] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Servicio_LineaNegocio_LineaNegocioId] FOREIGN KEY ([LineaNegocioId]) REFERENCES [LineaNegocio] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [ConciliacionActivoFijo] (
    [Id] int NOT NULL IDENTITY,
    [Folio] nvarchar(max) NULL,
    [Fecha] datetime2 NOT NULL,
    [UbicacionId] int NULL,
    [Estatus] bit NOT NULL,
    CONSTRAINT [PK_ConciliacionActivoFijo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ConciliacionActivoFijo_Ubicacion_UbicacionId] FOREIGN KEY ([UbicacionId]) REFERENCES [Ubicacion] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [ConciliacionInsumo] (
    [Id] int NOT NULL IDENTITY,
    [Folio] nvarchar(max) NULL,
    [Fecha] datetime2 NOT NULL,
    [UbicacionId] int NULL,
    [Estatus] bit NOT NULL,
    CONSTRAINT [PK_ConciliacionInsumo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ConciliacionInsumo_Ubicacion_UbicacionId] FOREIGN KEY ([UbicacionId]) REFERENCES [Ubicacion] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [EntradaActivoFijo] (
    [Id] int NOT NULL IDENTITY,
    [Remision] nvarchar(max) NULL,
    [Tipo] nvarchar(max) NULL,
    [UbicacionId] int NULL,
    [FechaRemision] datetime2 NOT NULL,
    [FechaRecepcion] datetime2 NOT NULL,
    [Incidencia] bit NOT NULL,
    [Observaciones] nvarchar(max) NULL,
    [Estatus] bit NOT NULL,
    CONSTRAINT [PK_EntradaActivoFijo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EntradaActivoFijo_Ubicacion_UbicacionId] FOREIGN KEY ([UbicacionId]) REFERENCES [Ubicacion] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [EntradaInsumo] (
    [Id] int NOT NULL IDENTITY,
    [Pedido] nvarchar(max) NULL,
    [Tipo] nvarchar(max) NULL,
    [UbicacionId] int NULL,
    [FechaPedido] datetime2 NOT NULL,
    [FechaRecepcion] datetime2 NOT NULL,
    [Incidencia] bit NOT NULL,
    [Observaciones] nvarchar(max) NULL,
    [Estatus] bit NOT NULL,
    CONSTRAINT [PK_EntradaInsumo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EntradaInsumo_Ubicacion_UbicacionId] FOREIGN KEY ([UbicacionId]) REFERENCES [Ubicacion] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [TraspasoActivoFijo] (
    [Id] int NOT NULL IDENTITY,
    [UbicacionOrigenId] int NULL,
    [UbicacionDestinoId] int NULL,
    [MotivoSalida] nvarchar(max) NULL,
    [Folio] nvarchar(max) NULL,
    [FechaSalida] datetime2 NOT NULL,
    [Paqueteria] nvarchar(max) NULL,
    [NumGuia] nvarchar(max) NULL,
    [FechaEnvio] datetime2 NOT NULL,
    [Estatus] bit NOT NULL,
    CONSTRAINT [PK_TraspasoActivoFijo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TraspasoActivoFijo_Ubicacion_UbicacionDestinoId] FOREIGN KEY ([UbicacionDestinoId]) REFERENCES [Ubicacion] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TraspasoActivoFijo_Ubicacion_UbicacionOrigenId] FOREIGN KEY ([UbicacionOrigenId]) REFERENCES [Ubicacion] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [TraspasoInsumo] (
    [Id] int NOT NULL IDENTITY,
    [UbicacionOrigenId] int NULL,
    [UbicacionDestinoId] int NULL,
    [MotivoSalida] nvarchar(max) NULL,
    [Folio] nvarchar(max) NULL,
    [FechaSalida] datetime2 NOT NULL,
    [Paqueteria] nvarchar(max) NULL,
    [NumGuia] nvarchar(max) NULL,
    [FechaEnvio] datetime2 NOT NULL,
    [Estatus] bit NOT NULL,
    CONSTRAINT [PK_TraspasoInsumo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TraspasoInsumo_Ubicacion_UbicacionDestinoId] FOREIGN KEY ([UbicacionDestinoId]) REFERENCES [Ubicacion] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TraspasoInsumo_Ubicacion_UbicacionOrigenId] FOREIGN KEY ([UbicacionOrigenId]) REFERENCES [Ubicacion] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    [perId] int NULL,
    [dirId] int NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUsers_Direccion_dirId] FOREIGN KEY ([dirId]) REFERENCES [Direccion] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AspNetUsers_Persona_perId] FOREIGN KEY ([perId]) REFERENCES [Persona] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [InventarioAF] (
    [Id] int NOT NULL IDENTITY,
    [ActivoFijoId] int NULL,
    [PersonaId] int NULL,
    [Estatus] bit NOT NULL,
    [FechaAlta] datetime2 NOT NULL,
    [Cantidad] int NOT NULL,
    [Observaciones] nvarchar(max) NULL,
    CONSTRAINT [PK_InventarioAF] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_InventarioAF_ActivoFijo_ActivoFijoId] FOREIGN KEY ([ActivoFijoId]) REFERENCES [ActivoFijo] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_InventarioAF_Persona_PersonaId] FOREIGN KEY ([PersonaId]) REFERENCES [Persona] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Contrato] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NULL,
    [Tipo] nvarchar(max) NULL,
    [Estatus] bit NOT NULL,
    [Opcional1] nvarchar(max) NULL,
    [Opcional2] nvarchar(max) NULL,
    [ClienteId] int NOT NULL,
    [ServicioId] int NOT NULL,
    CONSTRAINT [PK_Contrato] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Contrato_Cliente_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Cliente] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Contrato_Servicio_ServicioId] FOREIGN KEY ([ServicioId]) REFERENCES [Servicio] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [InventarioActivoFijo] (
    [Id] int NOT NULL IDENTITY,
    [ActivoFijoId] int NULL,
    [Teorico] int NOT NULL,
    [Fisico] int NOT NULL,
    [Ajuste] int NOT NULL,
    [Estatus] bit NOT NULL,
    [ConciliacionActivoFijoId] int NULL,
    CONSTRAINT [PK_InventarioActivoFijo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_InventarioActivoFijo_ActivoFijo_ActivoFijoId] FOREIGN KEY ([ActivoFijoId]) REFERENCES [ActivoFijo] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_InventarioActivoFijo_ConciliacionActivoFijo_ConciliacionActivoFijoId] FOREIGN KEY ([ConciliacionActivoFijoId]) REFERENCES [ConciliacionActivoFijo] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [InventarioInsumo] (
    [Id] int NOT NULL IDENTITY,
    [InsumoId] int NULL,
    [Teorico] int NOT NULL,
    [Fisico] int NOT NULL,
    [Ajuste] int NOT NULL,
    [Estatus] bit NOT NULL,
    [ConciliacionInsumoId] int NULL,
    CONSTRAINT [PK_InventarioInsumo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_InventarioInsumo_ConciliacionInsumo_ConciliacionInsumoId] FOREIGN KEY ([ConciliacionInsumoId]) REFERENCES [ConciliacionInsumo] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_InventarioInsumo_Insumo_InsumoId] FOREIGN KEY ([InsumoId]) REFERENCES [Insumo] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [DetalleActivoFijo] (
    [Id] int NOT NULL IDENTITY,
    [ReferenciaId] int NULL,
    [Descripcion] nvarchar(max) NULL,
    [Cantidad] int NOT NULL,
    [NumeroSerie] nvarchar(max) NULL,
    [Unidad] nvarchar(max) NULL,
    [Arrendamiento] bit NOT NULL,
    [Observaciones] nvarchar(max) NULL,
    [Estatus] bit NOT NULL,
    [EntradaActivoFijoId] int NULL,
    CONSTRAINT [PK_DetalleActivoFijo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DetalleActivoFijo_EntradaActivoFijo_EntradaActivoFijoId] FOREIGN KEY ([EntradaActivoFijoId]) REFERENCES [EntradaActivoFijo] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_DetalleActivoFijo_ActivoFijo_ReferenciaId] FOREIGN KEY ([ReferenciaId]) REFERENCES [ActivoFijo] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [DetalleInsumo] (
    [Id] int NOT NULL IDENTITY,
    [ReferenciaId] int NULL,
    [ClaveInsumo] nvarchar(max) NULL,
    [Cantidad] int NOT NULL,
    [Unidad] nvarchar(max) NULL,
    [Observaciones] nvarchar(max) NULL,
    [Estatus] bit NOT NULL,
    [EntradaInsumoId] int NULL,
    CONSTRAINT [PK_DetalleInsumo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DetalleInsumo_EntradaInsumo_EntradaInsumoId] FOREIGN KEY ([EntradaInsumoId]) REFERENCES [EntradaInsumo] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_DetalleInsumo_Insumo_ReferenciaId] FOREIGN KEY ([ReferenciaId]) REFERENCES [Insumo] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [TraspasoDetalleActivoFijo] (
    [Id] int NOT NULL IDENTITY,
    [ActivoFijoId] int NULL,
    [Estatus] bit NOT NULL,
    [TraspasoActivoFijoId] int NULL,
    CONSTRAINT [PK_TraspasoDetalleActivoFijo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TraspasoDetalleActivoFijo_ActivoFijo_ActivoFijoId] FOREIGN KEY ([ActivoFijoId]) REFERENCES [ActivoFijo] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TraspasoDetalleActivoFijo_TraspasoActivoFijo_TraspasoActivoFijoId] FOREIGN KEY ([TraspasoActivoFijoId]) REFERENCES [TraspasoActivoFijo] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [TraspasoDetalleInsumo] (
    [Id] int NOT NULL IDENTITY,
    [InsumoId] int NULL,
    [Estatus] bit NOT NULL,
    [TraspasoInsumoId] int NULL,
    CONSTRAINT [PK_TraspasoDetalleInsumo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TraspasoDetalleInsumo_Insumo_InsumoId] FOREIGN KEY ([InsumoId]) REFERENCES [Insumo] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TraspasoDetalleInsumo_TraspasoInsumo_TraspasoInsumoId] FOREIGN KEY ([TraspasoInsumoId]) REFERENCES [TraspasoInsumo] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [ConfiguracionServicio] (
    [Id] int NOT NULL IDENTITY,
    [ClienteId] int NULL,
    [ContratoId] int NULL,
    [UbicacionId] int NULL,
    [Estatus] bit NOT NULL,
    CONSTRAINT [PK_ConfiguracionServicio] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ConfiguracionServicio_Cliente_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Cliente] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ConfiguracionServicio_Contrato_ContratoId] FOREIGN KEY ([ContratoId]) REFERENCES [Contrato] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ConfiguracionServicio_Ubicacion_UbicacionId] FOREIGN KEY ([UbicacionId]) REFERENCES [Ubicacion] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [OrdenServicio] (
    [Id] int NOT NULL IDENTITY,
    [Folio] nvarchar(max) NULL,
    [FechaInicio] datetime2 NULL,
    [FechaFin] datetime2 NULL,
    [ClienteId] int NULL,
    [ContratoId] int NULL,
    [UbicacionId] int NULL,
    [LineaNegocioId] int NULL,
    [ServicioId] int NULL,
    [Tipo] nvarchar(max) NULL,
    [EstatusServicio] nvarchar(max) NULL,
    [Observaciones] nvarchar(max) NULL,
    [ContactoNombre] nvarchar(max) NULL,
    [ContactoAP] nvarchar(max) NULL,
    [ContactoAM] nvarchar(max) NULL,
    [ContactoEmail] nvarchar(max) NULL,
    [ContactoTelefono] nvarchar(max) NULL,
    [NombreCompletoCC1] nvarchar(max) NULL,
    [EmailCC1] nvarchar(max) NULL,
    [NombreCompletoCC2] nvarchar(max) NULL,
    [EmailCC2] nvarchar(max) NULL,
    [Opcional1] nvarchar(max) NULL,
    [Opcional2] nvarchar(max) NULL,
    [Opcional3] nvarchar(max) NULL,
    [Opcional4] nvarchar(max) NULL,
    [Estatus] bit NOT NULL,
    [PersonaComercialId] int NULL,
    [PersonaValidaId] int NULL,
    [FechaAdministrativa] datetime2 NOT NULL,
    CONSTRAINT [PK_OrdenServicio] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrdenServicio_Cliente_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Cliente] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OrdenServicio_Contrato_ContratoId] FOREIGN KEY ([ContratoId]) REFERENCES [Contrato] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OrdenServicio_LineaNegocio_LineaNegocioId] FOREIGN KEY ([LineaNegocioId]) REFERENCES [LineaNegocio] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OrdenServicio_Persona_PersonaComercialId] FOREIGN KEY ([PersonaComercialId]) REFERENCES [Persona] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OrdenServicio_Persona_PersonaValidaId] FOREIGN KEY ([PersonaValidaId]) REFERENCES [Persona] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OrdenServicio_Servicio_ServicioId] FOREIGN KEY ([ServicioId]) REFERENCES [Servicio] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OrdenServicio_Ubicacion_UbicacionId] FOREIGN KEY ([UbicacionId]) REFERENCES [Ubicacion] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [DetalleConfiguracionServicio] (
    [Id] int NOT NULL IDENTITY,
    [LineaNegocioId] int NULL,
    [ServicioId] int NULL,
    [CostoServicio] decimal(18, 2) NOT NULL,
    [PrecioServicio] decimal(18, 2) NOT NULL,
    [MinimoServicio] int NOT NULL,
    [MaximoServicio] int NOT NULL,
    [Opcional1] nvarchar(max) NULL,
    [Opcional2] int NOT NULL,
    [Estatus] bit NOT NULL,
    [ConfiguracionServicioId] int NULL,
    CONSTRAINT [PK_DetalleConfiguracionServicio] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DetalleConfiguracionServicio_ConfiguracionServicio_ConfiguracionServicioId] FOREIGN KEY ([ConfiguracionServicioId]) REFERENCES [ConfiguracionServicio] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_DetalleConfiguracionServicio_LineaNegocio_LineaNegocioId] FOREIGN KEY ([LineaNegocioId]) REFERENCES [LineaNegocio] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_DetalleConfiguracionServicio_Servicio_ServicioId] FOREIGN KEY ([ServicioId]) REFERENCES [Servicio] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [BitacoraEstatus] (
    [Id] int NOT NULL IDENTITY,
    [OsId] int NULL,
    [Folio] nvarchar(max) NULL,
    [QuienCambiaId] int NULL,
    [Email] nvarchar(max) NULL,
    [De] nvarchar(max) NULL,
    [A] nvarchar(max) NULL,
    [FechaAdministrativa] datetime2 NOT NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_BitacoraEstatus] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BitacoraEstatus_OrdenServicio_OsId] FOREIGN KEY ([OsId]) REFERENCES [OrdenServicio] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_BitacoraEstatus_Persona_QuienCambiaId] FOREIGN KEY ([QuienCambiaId]) REFERENCES [Persona] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Operador] (
    [Id] int NOT NULL IDENTITY,
    [Estatus] bit NOT NULL,
    [OrdenServicioId] int NULL,
    [PersonaId] int NULL,
    [Hora] datetime2 NOT NULL,
    [FechaAdministrativa] datetime2 NOT NULL,
    CONSTRAINT [PK_Operador] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Operador_OrdenServicio_OrdenServicioId] FOREIGN KEY ([OrdenServicioId]) REFERENCES [OrdenServicio] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Operador_Persona_PersonaId] FOREIGN KEY ([PersonaId]) REFERENCES [Persona] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [OrdenActivoFijo] (
    [Id] int NOT NULL IDENTITY,
    [ActivoFijoId] int NULL,
    [Estatus] bit NOT NULL,
    [OrdenServicioId] int NULL,
    CONSTRAINT [PK_OrdenActivoFijo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrdenActivoFijo_ActivoFijo_ActivoFijoId] FOREIGN KEY ([ActivoFijoId]) REFERENCES [ActivoFijo] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OrdenActivoFijo_OrdenServicio_OrdenServicioId] FOREIGN KEY ([OrdenServicioId]) REFERENCES [OrdenServicio] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [OrdenInsumo] (
    [Id] int NOT NULL IDENTITY,
    [InsumoId] int NULL,
    [Cantidad] int NOT NULL,
    [Estatus] bit NOT NULL,
    [OrdenServicioId] int NULL,
    CONSTRAINT [PK_OrdenInsumo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrdenInsumo_Insumo_InsumoId] FOREIGN KEY ([InsumoId]) REFERENCES [Insumo] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OrdenInsumo_OrdenServicio_OrdenServicioId] FOREIGN KEY ([OrdenServicioId]) REFERENCES [OrdenServicio] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [OrdenPersona] (
    [Id] int NOT NULL IDENTITY,
    [PersonaId] int NULL,
    [OrdenServicioId] int NULL,
    [Estatus] bit NOT NULL,
    CONSTRAINT [PK_OrdenPersona] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrdenPersona_OrdenServicio_OrdenServicioId] FOREIGN KEY ([OrdenServicioId]) REFERENCES [OrdenServicio] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OrdenPersona_Persona_PersonaId] FOREIGN KEY ([PersonaId]) REFERENCES [Persona] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Estado] (
    [Id] int NOT NULL IDENTITY,
    [NuevoEstado] nvarchar(max) NULL,
    [ComentarioNuevoEstado] nvarchar(max) NULL,
    [OperadorId] int NULL,
    CONSTRAINT [PK_Estado] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Estado_Operador_OperadorId] FOREIGN KEY ([OperadorId]) REFERENCES [Operador] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Archivo] (
    [Id] int NOT NULL IDENTITY,
    [Path] nvarchar(max) NULL,
    [Type] nvarchar(max) NULL,
    [Name] nvarchar(max) NULL,
    [Size] bigint NOT NULL,
    [File] varbinary(max) NULL,
    [LastModified] datetime2 NOT NULL,
    [EstadoId] int NULL,
    CONSTRAINT [PK_Archivo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Archivo_Estado_EstadoId] FOREIGN KEY ([EstadoId]) REFERENCES [Estado] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Archivo_EstadoId] ON [Archivo] ([EstadoId]);

GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

GO

CREATE INDEX [IX_AspNetUsers_dirId] ON [AspNetUsers] ([dirId]);

GO

CREATE INDEX [IX_AspNetUsers_perId] ON [AspNetUsers] ([perId]);

GO

CREATE INDEX [IX_BitacoraEstatus_OsId] ON [BitacoraEstatus] ([OsId]);

GO

CREATE INDEX [IX_BitacoraEstatus_QuienCambiaId] ON [BitacoraEstatus] ([QuienCambiaId]);

GO

CREATE INDEX [IX_ConciliacionActivoFijo_UbicacionId] ON [ConciliacionActivoFijo] ([UbicacionId]);

GO

CREATE INDEX [IX_ConciliacionInsumo_UbicacionId] ON [ConciliacionInsumo] ([UbicacionId]);

GO

CREATE INDEX [IX_ConfiguracionServicio_ClienteId] ON [ConfiguracionServicio] ([ClienteId]);

GO

CREATE INDEX [IX_ConfiguracionServicio_ContratoId] ON [ConfiguracionServicio] ([ContratoId]);

GO

CREATE INDEX [IX_ConfiguracionServicio_UbicacionId] ON [ConfiguracionServicio] ([UbicacionId]);

GO

CREATE INDEX [IX_Contrato_ClienteId] ON [Contrato] ([ClienteId]);

GO

CREATE INDEX [IX_Contrato_ServicioId] ON [Contrato] ([ServicioId]);

GO

CREATE INDEX [IX_DetalleActivoFijo_EntradaActivoFijoId] ON [DetalleActivoFijo] ([EntradaActivoFijoId]);

GO

CREATE INDEX [IX_DetalleActivoFijo_ReferenciaId] ON [DetalleActivoFijo] ([ReferenciaId]);

GO

CREATE INDEX [IX_DetalleConfiguracionServicio_ConfiguracionServicioId] ON [DetalleConfiguracionServicio] ([ConfiguracionServicioId]);

GO

CREATE INDEX [IX_DetalleConfiguracionServicio_LineaNegocioId] ON [DetalleConfiguracionServicio] ([LineaNegocioId]);

GO

CREATE INDEX [IX_DetalleConfiguracionServicio_ServicioId] ON [DetalleConfiguracionServicio] ([ServicioId]);

GO

CREATE INDEX [IX_DetalleInsumo_EntradaInsumoId] ON [DetalleInsumo] ([EntradaInsumoId]);

GO

CREATE INDEX [IX_DetalleInsumo_ReferenciaId] ON [DetalleInsumo] ([ReferenciaId]);

GO

CREATE INDEX [IX_EntradaActivoFijo_UbicacionId] ON [EntradaActivoFijo] ([UbicacionId]);

GO

CREATE INDEX [IX_EntradaInsumo_UbicacionId] ON [EntradaInsumo] ([UbicacionId]);

GO

CREATE INDEX [IX_Estado_OperadorId] ON [Estado] ([OperadorId]);

GO

CREATE INDEX [IX_InventarioActivoFijo_ActivoFijoId] ON [InventarioActivoFijo] ([ActivoFijoId]);

GO

CREATE INDEX [IX_InventarioActivoFijo_ConciliacionActivoFijoId] ON [InventarioActivoFijo] ([ConciliacionActivoFijoId]);

GO

CREATE INDEX [IX_InventarioAF_ActivoFijoId] ON [InventarioAF] ([ActivoFijoId]);

GO

CREATE INDEX [IX_InventarioAF_PersonaId] ON [InventarioAF] ([PersonaId]);

GO

CREATE INDEX [IX_InventarioInsumo_ConciliacionInsumoId] ON [InventarioInsumo] ([ConciliacionInsumoId]);

GO

CREATE INDEX [IX_InventarioInsumo_InsumoId] ON [InventarioInsumo] ([InsumoId]);

GO

CREATE INDEX [IX_Operador_OrdenServicioId] ON [Operador] ([OrdenServicioId]);

GO

CREATE INDEX [IX_Operador_PersonaId] ON [Operador] ([PersonaId]);

GO

CREATE INDEX [IX_OrdenActivoFijo_ActivoFijoId] ON [OrdenActivoFijo] ([ActivoFijoId]);

GO

CREATE INDEX [IX_OrdenActivoFijo_OrdenServicioId] ON [OrdenActivoFijo] ([OrdenServicioId]);

GO

CREATE INDEX [IX_OrdenInsumo_InsumoId] ON [OrdenInsumo] ([InsumoId]);

GO

CREATE INDEX [IX_OrdenInsumo_OrdenServicioId] ON [OrdenInsumo] ([OrdenServicioId]);

GO

CREATE INDEX [IX_OrdenPersona_OrdenServicioId] ON [OrdenPersona] ([OrdenServicioId]);

GO

CREATE INDEX [IX_OrdenPersona_PersonaId] ON [OrdenPersona] ([PersonaId]);

GO

CREATE INDEX [IX_OrdenServicio_ClienteId] ON [OrdenServicio] ([ClienteId]);

GO

CREATE INDEX [IX_OrdenServicio_ContratoId] ON [OrdenServicio] ([ContratoId]);

GO

CREATE INDEX [IX_OrdenServicio_LineaNegocioId] ON [OrdenServicio] ([LineaNegocioId]);

GO

CREATE INDEX [IX_OrdenServicio_PersonaComercialId] ON [OrdenServicio] ([PersonaComercialId]);

GO

CREATE INDEX [IX_OrdenServicio_PersonaValidaId] ON [OrdenServicio] ([PersonaValidaId]);

GO

CREATE INDEX [IX_OrdenServicio_ServicioId] ON [OrdenServicio] ([ServicioId]);

GO

CREATE INDEX [IX_OrdenServicio_UbicacionId] ON [OrdenServicio] ([UbicacionId]);

GO

CREATE INDEX [IX_Persona_DirId] ON [Persona] ([DirId]);

GO

CREATE INDEX [IX_Servicio_LineaNegocioId] ON [Servicio] ([LineaNegocioId]);

GO

CREATE INDEX [IX_TraspasoActivoFijo_UbicacionDestinoId] ON [TraspasoActivoFijo] ([UbicacionDestinoId]);

GO

CREATE INDEX [IX_TraspasoActivoFijo_UbicacionOrigenId] ON [TraspasoActivoFijo] ([UbicacionOrigenId]);

GO

CREATE INDEX [IX_TraspasoDetalleActivoFijo_ActivoFijoId] ON [TraspasoDetalleActivoFijo] ([ActivoFijoId]);

GO

CREATE INDEX [IX_TraspasoDetalleActivoFijo_TraspasoActivoFijoId] ON [TraspasoDetalleActivoFijo] ([TraspasoActivoFijoId]);

GO

CREATE INDEX [IX_TraspasoDetalleInsumo_InsumoId] ON [TraspasoDetalleInsumo] ([InsumoId]);

GO

CREATE INDEX [IX_TraspasoDetalleInsumo_TraspasoInsumoId] ON [TraspasoDetalleInsumo] ([TraspasoInsumoId]);

GO

CREATE INDEX [IX_TraspasoInsumo_UbicacionDestinoId] ON [TraspasoInsumo] ([UbicacionDestinoId]);

GO

CREATE INDEX [IX_TraspasoInsumo_UbicacionOrigenId] ON [TraspasoInsumo] ([UbicacionOrigenId]);

GO

CREATE INDEX [IX_Ubicacion_ClienteId] ON [Ubicacion] ([ClienteId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200511222401_primera', N'3.1.23');

GO

ALTER TABLE [Servicio] DROP CONSTRAINT [FK_Servicio_LineaNegocio_LineaNegocioId];

GO

DROP INDEX [IX_Servicio_LineaNegocioId] ON [Servicio];
DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Servicio]') AND [c].[name] = N'LineaNegocioId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Servicio] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Servicio] ALTER COLUMN [LineaNegocioId] int NOT NULL;
CREATE INDEX [IX_Servicio_LineaNegocioId] ON [Servicio] ([LineaNegocioId]);

GO

ALTER TABLE [Archivo] ADD [Exif] nvarchar(max) NULL;

GO

ALTER TABLE [Servicio] ADD CONSTRAINT [FK_Servicio_LineaNegocio_LineaNegocioId] FOREIGN KEY ([LineaNegocioId]) REFERENCES [LineaNegocio] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200520033443_ArchivoActualizado', N'3.1.23');

GO

ALTER TABLE [Archivo] ADD [ExifBi] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200521004509_Modificacion Archivo - ExifBi', N'3.1.23');

GO

ALTER TABLE [Ubicacion] ADD [Latitude] nvarchar(max) NULL;

GO

ALTER TABLE [Ubicacion] ADD [Longitude] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200522151112_ModifUbicacion', N'3.1.23');

GO

CREATE TABLE [SemaphoreParams] (
    [Id] int NOT NULL IDENTITY,
    [LlegadaVerde] int NOT NULL,
    [LlegadaAmarillo] int NOT NULL,
    [LlegadaRojo] int NOT NULL,
    [SalidaVerde] int NOT NULL,
    [SalidaAmarillo] int NOT NULL,
    [SalidaRojo] int NOT NULL,
    CONSTRAINT [PK_SemaphoreParams] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200527202733_SemaphoreParams', N'3.1.23');

GO

ALTER TABLE [Persona] ADD [Adscripcion] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200528225909_PersonaAdscripcion', N'3.1.23');

GO

ALTER TABLE [OrdenInsumo] ADD [Caducidad] nvarchar(max) NULL;

GO

ALTER TABLE [OrdenInsumo] ADD [Lote] nvarchar(max) NULL;

GO

CREATE TABLE [Kit] (
    [Id] int NOT NULL IDENTITY,
    [Descripcion] nvarchar(max) NULL,
    [CreaId] int NULL,
    [Estatus] bit NOT NULL,
    [FechaAdmin] datetime2 NOT NULL,
    [Observaciones] nvarchar(max) NULL,
    CONSTRAINT [PK_Kit] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Kit_Persona_CreaId] FOREIGN KEY ([CreaId]) REFERENCES [Persona] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Lote] (
    [Id] int NOT NULL IDENTITY,
    [Descripcion] nvarchar(max) NULL,
    [InsumoId] int NULL,
    [Caducidad] datetime2 NOT NULL,
    [PersonaId] int NULL,
    [Estatus] bit NOT NULL,
    [FechaAlta] datetime2 NOT NULL,
    [Cantidad] int NOT NULL,
    [Observaciones] nvarchar(max) NULL,
    CONSTRAINT [PK_Lote] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Lote_Insumo_InsumoId] FOREIGN KEY ([InsumoId]) REFERENCES [Insumo] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Lote_Persona_PersonaId] FOREIGN KEY ([PersonaId]) REFERENCES [Persona] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Paquete] (
    [Id] int NOT NULL IDENTITY,
    [Clasificacion] nvarchar(max) NULL,
    [Descripcion] nvarchar(max) NULL,
    [CreaId] int NULL,
    [Estatus] bit NOT NULL,
    [FechaAdmin] datetime2 NOT NULL,
    [Observaciones] nvarchar(max) NULL,
    CONSTRAINT [PK_Paquete] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Paquete_Persona_CreaId] FOREIGN KEY ([CreaId]) REFERENCES [Persona] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [KitInsumo] (
    [Id] int NOT NULL IDENTITY,
    [InsumoId] int NULL,
    [KitId] int NULL,
    [Cantidad] int NOT NULL,
    [Estatus] bit NOT NULL,
    [FechaAdministrativa] datetime2 NOT NULL,
    CONSTRAINT [PK_KitInsumo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_KitInsumo_Insumo_InsumoId] FOREIGN KEY ([InsumoId]) REFERENCES [Insumo] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_KitInsumo_Kit_KitId] FOREIGN KEY ([KitId]) REFERENCES [Kit] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [PaqueteInsumo] (
    [Id] int NOT NULL IDENTITY,
    [Estatus] bit NOT NULL,
    [InsumoId] int NULL,
    [PaqueteId] int NULL,
    [Cantidad] int NOT NULL,
    CONSTRAINT [PK_PaqueteInsumo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PaqueteInsumo_Insumo_InsumoId] FOREIGN KEY ([InsumoId]) REFERENCES [Insumo] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_PaqueteInsumo_Paquete_PaqueteId] FOREIGN KEY ([PaqueteId]) REFERENCES [Paquete] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Kit_CreaId] ON [Kit] ([CreaId]);

GO

CREATE INDEX [IX_KitInsumo_InsumoId] ON [KitInsumo] ([InsumoId]);

GO

CREATE INDEX [IX_KitInsumo_KitId] ON [KitInsumo] ([KitId]);

GO

CREATE INDEX [IX_Lote_InsumoId] ON [Lote] ([InsumoId]);

GO

CREATE INDEX [IX_Lote_PersonaId] ON [Lote] ([PersonaId]);

GO

CREATE INDEX [IX_Paquete_CreaId] ON [Paquete] ([CreaId]);

GO

CREATE INDEX [IX_PaqueteInsumo_InsumoId] ON [PaqueteInsumo] ([InsumoId]);

GO

CREATE INDEX [IX_PaqueteInsumo_PaqueteId] ON [PaqueteInsumo] ([PaqueteId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200618050032_MigracionHistoryInventario', N'3.1.23');

GO

ALTER TABLE [ActivoFijo] ADD [NumeroSerie] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200618053008_ActivoFijoUpdated', N'3.1.23');

GO

CREATE TABLE [ClienteIdentity] (
    [Id] int NOT NULL IDENTITY,
    [ClienteId] int NULL,
    [CuentaUsuarioId] nvarchar(450) NULL,
    [Estatus] bit NOT NULL,
    [FechaAdministrativa] datetime2 NOT NULL,
    CONSTRAINT [PK_ClienteIdentity] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClienteIdentity_Cliente_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Cliente] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ClienteIdentity_AspNetUsers_CuentaUsuarioId] FOREIGN KEY ([CuentaUsuarioId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_ClienteIdentity_ClienteId] ON [ClienteIdentity] ([ClienteId]);

GO

CREATE INDEX [IX_ClienteIdentity_CuentaUsuarioId] ON [ClienteIdentity] ([CuentaUsuarioId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200618213119_ClienteIdentity', N'3.1.23');

GO

ALTER TABLE [Settings] ADD [UsoDeKits] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

ALTER TABLE [Settings] ADD [UsoDePaquetes] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200702231153_UpgradeSettings', N'3.1.23');

GO

ALTER TABLE [Archivo] ADD [EstructuraExifId] int NULL;

GO

CREATE TABLE [EstructuraExifBi] (
    [Id] int NOT NULL IDENTITY,
    [DateTimeOriginalId] int NOT NULL,
    [DateTimeOriginal] datetime2 NOT NULL,
    [GPSLongitudeId] int NOT NULL,
    [GPSLongitudeValue] nvarchar(max) NULL,
    [GPSLongitudeDescription] float NOT NULL,
    [GPSLongitudeRefId] int NOT NULL,
    [GPSLongitudeRefValue] nvarchar(max) NULL,
    [GPSLongitudeRefDescription] nvarchar(max) NULL,
    [GPSLatitudeId] int NOT NULL,
    [GPSLatitudeValue] nvarchar(max) NULL,
    [GPSLatitudeDescription] nvarchar(max) NULL,
    [GPSLatitudeRefId] int NOT NULL,
    [GPSLatitudeRefValue] nvarchar(max) NULL,
    [GPSLatitudeRefDescription] nvarchar(max) NULL,
    [GPSAltitudeId] int NOT NULL,
    [GPSAltitudeValue] nvarchar(max) NULL,
    [GPSAltitudeDescription] nvarchar(max) NULL,
    [GPSAltitudeRefId] int NOT NULL,
    [GPSAltitudeRefValue] int NOT NULL,
    [GPSAltitudeRefDescription] nvarchar(max) NULL,
    CONSTRAINT [PK_EstructuraExifBi] PRIMARY KEY ([Id])
);

GO

CREATE INDEX [IX_Archivo_EstructuraExifId] ON [Archivo] ([EstructuraExifId]);

GO

ALTER TABLE [Archivo] ADD CONSTRAINT [FK_Archivo_EstructuraExifBi_EstructuraExifId] FOREIGN KEY ([EstructuraExifId]) REFERENCES [EstructuraExifBi] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200707143209_EstructuraExifBi', N'3.1.23');

GO

ALTER TABLE [OrdenInsumo] ADD [LoteDId] int NULL;

GO

CREATE INDEX [IX_OrdenInsumo_LoteDId] ON [OrdenInsumo] ([LoteDId]);

GO

ALTER TABLE [OrdenInsumo] ADD CONSTRAINT [FK_OrdenInsumo_Lote_LoteDId] FOREIGN KEY ([LoteDId]) REFERENCES [Lote] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200722215742_InsumoModelUpgrated', N'3.1.23');

GO

ALTER TABLE [OrdenInsumo] DROP CONSTRAINT [FK_OrdenInsumo_Lote_LoteDId];

GO

DROP INDEX [IX_OrdenInsumo_LoteDId] ON [OrdenInsumo];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OrdenInsumo]') AND [c].[name] = N'LoteDId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [OrdenInsumo] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [OrdenInsumo] DROP COLUMN [LoteDId];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200723141558_RemoveLoteFromOrdenInsumo', N'3.1.23');

GO

ALTER TABLE [OrdenInsumo] ADD [LoteTypeId] int NULL;

GO

CREATE INDEX [IX_OrdenInsumo_LoteTypeId] ON [OrdenInsumo] ([LoteTypeId]);

GO

ALTER TABLE [OrdenInsumo] ADD CONSTRAINT [FK_OrdenInsumo_Lote_LoteTypeId] FOREIGN KEY ([LoteTypeId]) REFERENCES [Lote] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200723184320_OrdenInsumoUpgraded', N'3.1.23');

GO

ALTER TABLE [Settings] ADD [FaceApiIterarRequestUntil90OrMore] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

ALTER TABLE [Settings] ADD [FaceApiMantenerHistorico] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

ALTER TABLE [Settings] ADD [FaceApiUso] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200728171929_SettingsUpgradedFaceApi', N'3.1.23');

GO

ALTER TABLE [Settings] ADD [FaceApiMinCantEntrenamiento] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [Persona] ADD [FaceApiCount] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [Persona] ADD [FaceApiId] nvarchar(max) NOT NULL DEFAULT N'SINFACEAPIID';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200730184601_PersonaUpgradedFaceApi', N'3.1.23');

GO

ALTER TABLE [Archivo] ADD [FaceApiFinalResponse] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200801030732_ArchivoUpgradedFaceApi', N'3.1.23');

GO

ALTER TABLE [Settings] ADD [CustomVisionUso] bit NOT NULL DEFAULT CAST(0 AS bit);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200804155413_SettingsUpgradedCustomVision', N'3.1.23');

GO

ALTER TABLE [Archivo] ADD [CustomVisionResult] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200805174740_ArchivoUpgradedCustomVision', N'3.1.23');

GO

CREATE TABLE [EstructuraCustomVision] (
    [Id] int NOT NULL IDENTITY,
    [BoundingBoxHeight] real NOT NULL,
    [BoundingBoxLeft] real NOT NULL,
    [BoundingBoxTop] real NOT NULL,
    [BoundingBoxWidth] real NOT NULL,
    [Probability] real NOT NULL,
    [TagId] nvarchar(max) NULL,
    [TagName] nvarchar(max) NULL,
    [ArchivoId] int NULL,
    CONSTRAINT [PK_EstructuraCustomVision] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EstructuraCustomVision_Archivo_ArchivoId] FOREIGN KEY ([ArchivoId]) REFERENCES [Archivo] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_EstructuraCustomVision_ArchivoId] ON [EstructuraCustomVision] ([ArchivoId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200806184733_EstructuraCustomVision', N'3.1.23');

GO

CREATE TABLE [Marca] (
    [Id] int NOT NULL IDENTITY,
    [Descripcion] nvarchar(max) NULL,
    [Estatus] bit NOT NULL,
    [CreadorPorId] nvarchar(450) NULL,
    [ModificadoPorId] nvarchar(450) NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    [Opcional1] nvarchar(max) NULL,
    [Opcional2] nvarchar(max) NULL,
    CONSTRAINT [PK_Marca] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Marca_AspNetUsers_CreadorPorId] FOREIGN KEY ([CreadorPorId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Marca_AspNetUsers_ModificadoPorId] FOREIGN KEY ([ModificadoPorId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [TipoProducto] (
    [Id] int NOT NULL IDENTITY,
    [Descripcion] nvarchar(max) NULL,
    [Estatus] bit NOT NULL,
    [CreadorPorId] nvarchar(450) NULL,
    [ModificadoPorId] nvarchar(450) NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    [Opcional1] nvarchar(max) NULL,
    [Opcional2] nvarchar(max) NULL,
    CONSTRAINT [PK_TipoProducto] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TipoProducto_AspNetUsers_CreadorPorId] FOREIGN KEY ([CreadorPorId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_TipoProducto_AspNetUsers_ModificadoPorId] FOREIGN KEY ([ModificadoPorId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Marca_CreadorPorId] ON [Marca] ([CreadorPorId]);

GO

CREATE INDEX [IX_Marca_ModificadoPorId] ON [Marca] ([ModificadoPorId]);

GO

CREATE INDEX [IX_TipoProducto_CreadorPorId] ON [TipoProducto] ([CreadorPorId]);

GO

CREATE INDEX [IX_TipoProducto_ModificadoPorId] ON [TipoProducto] ([ModificadoPorId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200818184245_MarcaTipoProducto', N'3.1.23');

GO

ALTER TABLE [Persona] ADD [ContactoClienteId] int NULL;

GO

CREATE TABLE [ContactoCliente] (
    [Id] int NOT NULL IDENTITY,
    [Estatus] bit NOT NULL,
    [ClienteId] int NULL,
    [CreadorPorId] int NULL,
    [ModificadoPorId] int NULL,
    [FechaCreacion] datetime2 NOT NULL,
    [FechaModificacion] datetime2 NOT NULL,
    CONSTRAINT [PK_ContactoCliente] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ContactoCliente_Cliente_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Cliente] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ContactoCliente_Persona_CreadorPorId] FOREIGN KEY ([CreadorPorId]) REFERENCES [Persona] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ContactoCliente_Persona_ModificadoPorId] FOREIGN KEY ([ModificadoPorId]) REFERENCES [Persona] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Persona_ContactoClienteId] ON [Persona] ([ContactoClienteId]);

GO

CREATE INDEX [IX_ContactoCliente_ClienteId] ON [ContactoCliente] ([ClienteId]);

GO

CREATE INDEX [IX_ContactoCliente_CreadorPorId] ON [ContactoCliente] ([CreadorPorId]);

GO

CREATE INDEX [IX_ContactoCliente_ModificadoPorId] ON [ContactoCliente] ([ModificadoPorId]);

GO

ALTER TABLE [Persona] ADD CONSTRAINT [FK_Persona_ContactoCliente_ContactoClienteId] FOREIGN KEY ([ContactoClienteId]) REFERENCES [ContactoCliente] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200820181340_ContactoCliente', N'3.1.23');

GO

DROP INDEX [IX_ContactoCliente_ModificadoPorId] ON [ContactoCliente];

GO

DROP INDEX [IX_ContactoCliente_CreadorPorId] ON [ContactoCliente];

GO

ALTER TABLE [ContactoCliente] DROP CONSTRAINT [FK_ContactoCliente_Persona_CreadorPorId];

GO

ALTER TABLE [ContactoCliente] DROP CONSTRAINT [FK_ContactoCliente_Persona_ModificadoPorId];

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ContactoCliente]') AND [c].[name] = N'ModificadoPorId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [ContactoCliente] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [ContactoCliente] ALTER COLUMN [ModificadoPorId] nvarchar(450) NULL;

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ContactoCliente]') AND [c].[name] = N'CreadorPorId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [ContactoCliente] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [ContactoCliente] ALTER COLUMN [CreadorPorId] nvarchar(450) NULL;

GO

ALTER TABLE [ContactoCliente] ADD [Opcional1] nvarchar(max) NULL;

GO

ALTER TABLE [ContactoCliente] ADD [Opcional2] nvarchar(max) NULL;

GO

ALTER TABLE [ContactoCliente] ADD CONSTRAINT [FK_ContactoCliente_AspNetUsers_CreadorPorId] FOREIGN KEY ([CreadorPorId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;

GO

ALTER TABLE [ContactoCliente] ADD CONSTRAINT [FK_ContactoCliente_AspNetUsers_ModificadoPorId] FOREIGN KEY ([ModificadoPorId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200820193330_ContactoClienteUpgraded', N'3.1.23');

GO

ALTER TABLE [EstructuraExifBi] ADD [Distancia] real NOT NULL DEFAULT CAST(0 AS real);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200903223644_EstructuraExifBiUpgraded', N'3.1.23');

GO

ALTER TABLE [Ubicacion] ADD [Distancia] real NOT NULL DEFAULT CAST(0 AS real);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200903224747_UbicacionUpgraded', N'3.1.23');

GO

CREATE TABLE [BulkUploadTemplate] (
    [Id] int NOT NULL IDENTITY,
    [Tipo] nvarchar(max) NULL,
    [Archivo] varbinary(max) NULL,
    [SettingsId] int NULL,
    CONSTRAINT [PK_BulkUploadTemplate] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BulkUploadTemplate_Settings_SettingsId] FOREIGN KEY ([SettingsId]) REFERENCES [Settings] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_BulkUploadTemplate_SettingsId] ON [BulkUploadTemplate] ([SettingsId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200904033359_SettingsUpgradeBulkUpload', N'3.1.23');

GO

ALTER TABLE [BulkUploadTemplate] ADD [CreadoPorId] nvarchar(450) NULL;

GO

ALTER TABLE [BulkUploadTemplate] ADD [FechaAdministrativa] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

ALTER TABLE [BulkUploadTemplate] ADD [Version] nvarchar(max) NULL;

GO

CREATE INDEX [IX_BulkUploadTemplate_CreadoPorId] ON [BulkUploadTemplate] ([CreadoPorId]);

GO

ALTER TABLE [BulkUploadTemplate] ADD CONSTRAINT [FK_BulkUploadTemplate_AspNetUsers_CreadoPorId] FOREIGN KEY ([CreadoPorId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200904172709_BulkLoadTemplateUpgraded', N'3.1.23');

GO

ALTER TABLE [BulkUploadTemplate] ADD [TamanoArchivo] bigint NOT NULL DEFAULT CAST(0 AS bigint);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200910192112_BulkUploadTemplate_Upgraded', N'3.1.23');

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Persona]') AND [c].[name] = N'TelefonoContacto');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Persona] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Persona] ALTER COLUMN [TelefonoContacto] nvarchar(10) NULL;

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Persona]') AND [c].[name] = N'Telefono');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Persona] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Persona] ALTER COLUMN [Telefono] nvarchar(10) NULL;

GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Persona]') AND [c].[name] = N'RFC');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Persona] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Persona] ALTER COLUMN [RFC] nvarchar(13) NULL;

GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Persona]') AND [c].[name] = N'Puesto');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Persona] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Persona] ALTER COLUMN [Puesto] nvarchar(50) NULL;

GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Persona]') AND [c].[name] = N'Paterno');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Persona] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Persona] ALTER COLUMN [Paterno] nvarchar(30) NULL;

GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Persona]') AND [c].[name] = N'Opcional2');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Persona] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Persona] ALTER COLUMN [Opcional2] nvarchar(200) NULL;

GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Persona]') AND [c].[name] = N'Opcional1');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Persona] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Persona] ALTER COLUMN [Opcional1] nvarchar(200) NULL;

GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Persona]') AND [c].[name] = N'Nombre');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Persona] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [Persona] ALTER COLUMN [Nombre] nvarchar(30) NULL;

GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Persona]') AND [c].[name] = N'Municipio');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Persona] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [Persona] ALTER COLUMN [Municipio] nvarchar(50) NULL;

GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Persona]') AND [c].[name] = N'Materno');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Persona] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [Persona] ALTER COLUMN [Materno] nvarchar(30) NULL;

GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Persona]') AND [c].[name] = N'EntidadFederativa');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Persona] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [Persona] ALTER COLUMN [EntidadFederativa] nvarchar(50) NULL;

GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Persona]') AND [c].[name] = N'Direccion');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Persona] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [Persona] ALTER COLUMN [Direccion] nvarchar(150) NULL;

GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Persona]') AND [c].[name] = N'Categoria');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Persona] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [Persona] ALTER COLUMN [Categoria] nvarchar(30) NULL;

GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Persona]') AND [c].[name] = N'CURP');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Persona] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [Persona] ALTER COLUMN [CURP] nvarchar(18) NULL;

GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Persona]') AND [c].[name] = N'Adscripcion');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [Persona] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [Persona] ALTER COLUMN [Adscripcion] nvarchar(30) NULL;

GO

DECLARE @var19 sysname;
SELECT @var19 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Contrato]') AND [c].[name] = N'Nombre');
IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [Contrato] DROP CONSTRAINT [' + @var19 + '];');
ALTER TABLE [Contrato] ALTER COLUMN [Nombre] nvarchar(30) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200923221536_PorSiHayAlgoPendiente', N'3.1.23');

GO

CREATE TABLE [OsRecurrente] (
    [Id] int NOT NULL IDENTITY,
    [OsOrigenId] int NOT NULL,
    [Periodo] nvarchar(max) NULL,
    CONSTRAINT [PK_OsRecurrente] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Oses] (
    [Id] int NOT NULL IDENTITY,
    [OsId] int NOT NULL,
    [OsRecurrenteId] int NULL,
    CONSTRAINT [PK_Oses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Oses_OsRecurrente_OsRecurrenteId] FOREIGN KEY ([OsRecurrenteId]) REFERENCES [OsRecurrente] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Oses_OsRecurrenteId] ON [Oses] ([OsRecurrenteId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220609161924_Osrecurrente', N'3.1.23');

GO

