using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace siges.Migrations
{
    public partial class primera : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivoFijo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    Clave = table.Column<string>(nullable: true),
                    Marca = table.Column<string>(nullable: true),
                    Tipo = table.Column<string>(nullable: true),
                    FechaFactura = table.Column<DateTime>(nullable: false),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Estatus = table.Column<bool>(nullable: false),
                    Opcional1 = table.Column<string>(nullable: true),
                    Opcional2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivoFijo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Administracion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<bool>(nullable: false),
                    OrdenServicioId = table.Column<int>(nullable: false),
                    PersonaId = table.Column<int>(nullable: false),
                    FechaAdministrativa = table.Column<DateTime>(nullable: false),
                    FacturaFolio = table.Column<string>(nullable: true),
                    FacturaFecha = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administracion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bitacora",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    EventDate = table.Column<DateTime>(nullable: false),
                    Event = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Section = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bitacora", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RazonSocial = table.Column<string>(nullable: true),
                    RFC = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Opcional1 = table.Column<string>(nullable: true),
                    Opcional2 = table.Column<string>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comercial",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<bool>(nullable: false),
                    OrdenServicioId = table.Column<int>(nullable: false),
                    PersonaId = table.Column<int>(nullable: false),
                    FechaAdministrativa = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comercial", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Direccion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    calle = table.Column<string>(nullable: true),
                    numero = table.Column<int>(nullable: false),
                    colonia = table.Column<string>(nullable: true),
                    cp = table.Column<int>(nullable: false),
                    municipio = table.Column<string>(nullable: true),
                    entidadFederativa = table.Column<string>(nullable: true),
                    estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direccion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Insumo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(nullable: true),
                    Clave = table.Column<string>(nullable: true),
                    Marca = table.Column<string>(nullable: true),
                    Tipo = table.Column<string>(nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Estatus = table.Column<bool>(nullable: false),
                    Opcional1 = table.Column<string>(nullable: true),
                    Opcional2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insumo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LineaNegocio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false),
                    Opcional1 = table.Column<string>(nullable: true),
                    Opcional2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineaNegocio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Marca = table.Column<string>(nullable: true),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    Estatus = table.Column<bool>(nullable: false),
                    Opcional1 = table.Column<string>(nullable: true),
                    Opcional2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Version = table.Column<string>(nullable: true),
                    FolioPrefix = table.Column<string>(nullable: true),
                    FolioDigitsLong = table.Column<int>(nullable: false),
                    EmailHost = table.Column<string>(nullable: true),
                    EmailPort = table.Column<string>(nullable: true),
                    EmailUser = table.Column<string>(nullable: true),
                    EmailPass = table.Column<string>(nullable: true),
                    RemainingDaysToUpload = table.Column<int>(nullable: false),
                    EmailEnableSSL = table.Column<bool>(nullable: false),
                    MaxCaractersFields = table.Column<int>(nullable: false),
                    ValidateMinimumDate = table.Column<bool>(nullable: false),
                    MinimumDateCriteria = table.Column<string>(nullable: true),
                    AttachmentFile1 = table.Column<byte[]>(nullable: true),
                    AttachmentFile1Name = table.Column<string>(nullable: true),
                    SendAttachmentFile = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ubicacion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Contacto = table.Column<string>(nullable: true),
                    ContactoTelefono = table.Column<string>(nullable: true),
                    ContactoEmail = table.Column<string>(nullable: true),
                    ContactoOpcional = table.Column<string>(nullable: true),
                    ContactoOpcionalTelefono = table.Column<string>(nullable: true),
                    ContactoOpcionalEmail = table.Column<string>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false),
                    ClienteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ubicacion_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Paterno = table.Column<string>(nullable: true),
                    Materno = table.Column<string>(nullable: true),
                    RFC = table.Column<string>(nullable: true),
                    CURP = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DirId = table.Column<int>(nullable: true),
                    ClaveEmpleado = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    TelefonoContacto = table.Column<string>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false),
                    Categoria = table.Column<string>(nullable: true),
                    Puesto = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    EntidadFederativa = table.Column<string>(nullable: true),
                    Municipio = table.Column<string>(nullable: true),
                    Sueldo = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Opcional1 = table.Column<string>(nullable: true),
                    Opcional2 = table.Column<string>(nullable: true),
                    Fotografia = table.Column<byte[]>(nullable: true),
                    UsuarioId = table.Column<int>(nullable: false),
                    PerfilId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persona_Direccion_DirId",
                        column: x => x.DirId,
                        principalTable: "Direccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Servicio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    LineaNegocioId = table.Column<int>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false),
                    Opcional1 = table.Column<string>(nullable: true),
                    Opcional2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servicio_LineaNegocio_LineaNegocioId",
                        column: x => x.LineaNegocioId,
                        principalTable: "LineaNegocio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConciliacionActivoFijo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Folio = table.Column<string>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    UbicacionId = table.Column<int>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConciliacionActivoFijo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConciliacionActivoFijo_Ubicacion_UbicacionId",
                        column: x => x.UbicacionId,
                        principalTable: "Ubicacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConciliacionInsumo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Folio = table.Column<string>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    UbicacionId = table.Column<int>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConciliacionInsumo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConciliacionInsumo_Ubicacion_UbicacionId",
                        column: x => x.UbicacionId,
                        principalTable: "Ubicacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntradaActivoFijo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Remision = table.Column<string>(nullable: true),
                    Tipo = table.Column<string>(nullable: true),
                    UbicacionId = table.Column<int>(nullable: true),
                    FechaRemision = table.Column<DateTime>(nullable: false),
                    FechaRecepcion = table.Column<DateTime>(nullable: false),
                    Incidencia = table.Column<bool>(nullable: false),
                    Observaciones = table.Column<string>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntradaActivoFijo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntradaActivoFijo_Ubicacion_UbicacionId",
                        column: x => x.UbicacionId,
                        principalTable: "Ubicacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntradaInsumo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Pedido = table.Column<string>(nullable: true),
                    Tipo = table.Column<string>(nullable: true),
                    UbicacionId = table.Column<int>(nullable: true),
                    FechaPedido = table.Column<DateTime>(nullable: false),
                    FechaRecepcion = table.Column<DateTime>(nullable: false),
                    Incidencia = table.Column<bool>(nullable: false),
                    Observaciones = table.Column<string>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntradaInsumo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntradaInsumo_Ubicacion_UbicacionId",
                        column: x => x.UbicacionId,
                        principalTable: "Ubicacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TraspasoActivoFijo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UbicacionOrigenId = table.Column<int>(nullable: true),
                    UbicacionDestinoId = table.Column<int>(nullable: true),
                    MotivoSalida = table.Column<string>(nullable: true),
                    Folio = table.Column<string>(nullable: true),
                    FechaSalida = table.Column<DateTime>(nullable: false),
                    Paqueteria = table.Column<string>(nullable: true),
                    NumGuia = table.Column<string>(nullable: true),
                    FechaEnvio = table.Column<DateTime>(nullable: false),
                    Estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraspasoActivoFijo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TraspasoActivoFijo_Ubicacion_UbicacionDestinoId",
                        column: x => x.UbicacionDestinoId,
                        principalTable: "Ubicacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TraspasoActivoFijo_Ubicacion_UbicacionOrigenId",
                        column: x => x.UbicacionOrigenId,
                        principalTable: "Ubicacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TraspasoInsumo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UbicacionOrigenId = table.Column<int>(nullable: true),
                    UbicacionDestinoId = table.Column<int>(nullable: true),
                    MotivoSalida = table.Column<string>(nullable: true),
                    Folio = table.Column<string>(nullable: true),
                    FechaSalida = table.Column<DateTime>(nullable: false),
                    Paqueteria = table.Column<string>(nullable: true),
                    NumGuia = table.Column<string>(nullable: true),
                    FechaEnvio = table.Column<DateTime>(nullable: false),
                    Estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraspasoInsumo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TraspasoInsumo_Ubicacion_UbicacionDestinoId",
                        column: x => x.UbicacionDestinoId,
                        principalTable: "Ubicacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TraspasoInsumo_Ubicacion_UbicacionOrigenId",
                        column: x => x.UbicacionOrigenId,
                        principalTable: "Ubicacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    perId = table.Column<int>(nullable: true),
                    dirId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Direccion_dirId",
                        column: x => x.dirId,
                        principalTable: "Direccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Persona_perId",
                        column: x => x.perId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventarioAF",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActivoFijoId = table.Column<int>(nullable: true),
                    PersonaId = table.Column<int>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false),
                    FechaAlta = table.Column<DateTime>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Observaciones = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventarioAF", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventarioAF_ActivoFijo_ActivoFijoId",
                        column: x => x.ActivoFijoId,
                        principalTable: "ActivoFijo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventarioAF_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contrato",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Tipo = table.Column<string>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false),
                    Opcional1 = table.Column<string>(nullable: true),
                    Opcional2 = table.Column<string>(nullable: true),
                    ClienteId = table.Column<int>(nullable: false),
                    ServicioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contrato_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contrato_Servicio_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventarioActivoFijo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActivoFijoId = table.Column<int>(nullable: true),
                    Teorico = table.Column<int>(nullable: false),
                    Fisico = table.Column<int>(nullable: false),
                    Ajuste = table.Column<int>(nullable: false),
                    Estatus = table.Column<bool>(nullable: false),
                    ConciliacionActivoFijoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventarioActivoFijo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventarioActivoFijo_ActivoFijo_ActivoFijoId",
                        column: x => x.ActivoFijoId,
                        principalTable: "ActivoFijo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventarioActivoFijo_ConciliacionActivoFijo_ConciliacionActivoFijoId",
                        column: x => x.ConciliacionActivoFijoId,
                        principalTable: "ConciliacionActivoFijo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventarioInsumo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InsumoId = table.Column<int>(nullable: true),
                    Teorico = table.Column<int>(nullable: false),
                    Fisico = table.Column<int>(nullable: false),
                    Ajuste = table.Column<int>(nullable: false),
                    Estatus = table.Column<bool>(nullable: false),
                    ConciliacionInsumoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventarioInsumo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventarioInsumo_ConciliacionInsumo_ConciliacionInsumoId",
                        column: x => x.ConciliacionInsumoId,
                        principalTable: "ConciliacionInsumo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventarioInsumo_Insumo_InsumoId",
                        column: x => x.InsumoId,
                        principalTable: "Insumo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetalleActivoFijo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReferenciaId = table.Column<int>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Cantidad = table.Column<int>(nullable: false),
                    NumeroSerie = table.Column<string>(nullable: true),
                    Unidad = table.Column<string>(nullable: true),
                    Arrendamiento = table.Column<bool>(nullable: false),
                    Observaciones = table.Column<string>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false),
                    EntradaActivoFijoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleActivoFijo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleActivoFijo_EntradaActivoFijo_EntradaActivoFijoId",
                        column: x => x.EntradaActivoFijoId,
                        principalTable: "EntradaActivoFijo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleActivoFijo_ActivoFijo_ReferenciaId",
                        column: x => x.ReferenciaId,
                        principalTable: "ActivoFijo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetalleInsumo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReferenciaId = table.Column<int>(nullable: true),
                    ClaveInsumo = table.Column<string>(nullable: true),
                    Cantidad = table.Column<int>(nullable: false),
                    Unidad = table.Column<string>(nullable: true),
                    Observaciones = table.Column<string>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false),
                    EntradaInsumoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleInsumo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleInsumo_EntradaInsumo_EntradaInsumoId",
                        column: x => x.EntradaInsumoId,
                        principalTable: "EntradaInsumo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleInsumo_Insumo_ReferenciaId",
                        column: x => x.ReferenciaId,
                        principalTable: "Insumo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TraspasoDetalleActivoFijo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActivoFijoId = table.Column<int>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false),
                    TraspasoActivoFijoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraspasoDetalleActivoFijo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TraspasoDetalleActivoFijo_ActivoFijo_ActivoFijoId",
                        column: x => x.ActivoFijoId,
                        principalTable: "ActivoFijo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TraspasoDetalleActivoFijo_TraspasoActivoFijo_TraspasoActivoFijoId",
                        column: x => x.TraspasoActivoFijoId,
                        principalTable: "TraspasoActivoFijo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TraspasoDetalleInsumo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InsumoId = table.Column<int>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false),
                    TraspasoInsumoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TraspasoDetalleInsumo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TraspasoDetalleInsumo_Insumo_InsumoId",
                        column: x => x.InsumoId,
                        principalTable: "Insumo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TraspasoDetalleInsumo_TraspasoInsumo_TraspasoInsumoId",
                        column: x => x.TraspasoInsumoId,
                        principalTable: "TraspasoInsumo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracionServicio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClienteId = table.Column<int>(nullable: true),
                    ContratoId = table.Column<int>(nullable: true),
                    UbicacionId = table.Column<int>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracionServicio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfiguracionServicio_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConfiguracionServicio_Contrato_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConfiguracionServicio_Ubicacion_UbicacionId",
                        column: x => x.UbicacionId,
                        principalTable: "Ubicacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdenServicio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Folio = table.Column<string>(nullable: true),
                    FechaInicio = table.Column<DateTime>(nullable: true),
                    FechaFin = table.Column<DateTime>(nullable: true),
                    ClienteId = table.Column<int>(nullable: true),
                    ContratoId = table.Column<int>(nullable: true),
                    UbicacionId = table.Column<int>(nullable: true),
                    LineaNegocioId = table.Column<int>(nullable: true),
                    ServicioId = table.Column<int>(nullable: true),
                    Tipo = table.Column<string>(nullable: true),
                    EstatusServicio = table.Column<string>(nullable: true),
                    Observaciones = table.Column<string>(nullable: true),
                    ContactoNombre = table.Column<string>(nullable: true),
                    ContactoAP = table.Column<string>(nullable: true),
                    ContactoAM = table.Column<string>(nullable: true),
                    ContactoEmail = table.Column<string>(nullable: true),
                    ContactoTelefono = table.Column<string>(nullable: true),
                    NombreCompletoCC1 = table.Column<string>(nullable: true),
                    EmailCC1 = table.Column<string>(nullable: true),
                    NombreCompletoCC2 = table.Column<string>(nullable: true),
                    EmailCC2 = table.Column<string>(nullable: true),
                    Opcional1 = table.Column<string>(nullable: true),
                    Opcional2 = table.Column<string>(nullable: true),
                    Opcional3 = table.Column<string>(nullable: true),
                    Opcional4 = table.Column<string>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false),
                    PersonaComercialId = table.Column<int>(nullable: true),
                    PersonaValidaId = table.Column<int>(nullable: true),
                    FechaAdministrativa = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenServicio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenServicio_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdenServicio_Contrato_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdenServicio_LineaNegocio_LineaNegocioId",
                        column: x => x.LineaNegocioId,
                        principalTable: "LineaNegocio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdenServicio_Persona_PersonaComercialId",
                        column: x => x.PersonaComercialId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdenServicio_Persona_PersonaValidaId",
                        column: x => x.PersonaValidaId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdenServicio_Servicio_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdenServicio_Ubicacion_UbicacionId",
                        column: x => x.UbicacionId,
                        principalTable: "Ubicacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetalleConfiguracionServicio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LineaNegocioId = table.Column<int>(nullable: true),
                    ServicioId = table.Column<int>(nullable: true),
                    CostoServicio = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    PrecioServicio = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    MinimoServicio = table.Column<int>(nullable: false),
                    MaximoServicio = table.Column<int>(nullable: false),
                    Opcional1 = table.Column<string>(nullable: true),
                    Opcional2 = table.Column<int>(nullable: false),
                    Estatus = table.Column<bool>(nullable: false),
                    ConfiguracionServicioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleConfiguracionServicio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleConfiguracionServicio_ConfiguracionServicio_ConfiguracionServicioId",
                        column: x => x.ConfiguracionServicioId,
                        principalTable: "ConfiguracionServicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleConfiguracionServicio_LineaNegocio_LineaNegocioId",
                        column: x => x.LineaNegocioId,
                        principalTable: "LineaNegocio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleConfiguracionServicio_Servicio_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BitacoraEstatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OsId = table.Column<int>(nullable: true),
                    Folio = table.Column<string>(nullable: true),
                    QuienCambiaId = table.Column<int>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    De = table.Column<string>(nullable: true),
                    A = table.Column<string>(nullable: true),
                    FechaAdministrativa = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BitacoraEstatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BitacoraEstatus_OrdenServicio_OsId",
                        column: x => x.OsId,
                        principalTable: "OrdenServicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BitacoraEstatus_Persona_QuienCambiaId",
                        column: x => x.QuienCambiaId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Operador",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<bool>(nullable: false),
                    OrdenServicioId = table.Column<int>(nullable: true),
                    PersonaId = table.Column<int>(nullable: true),
                    Hora = table.Column<DateTime>(nullable: false),
                    FechaAdministrativa = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operador_OrdenServicio_OrdenServicioId",
                        column: x => x.OrdenServicioId,
                        principalTable: "OrdenServicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Operador_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdenActivoFijo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActivoFijoId = table.Column<int>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false),
                    OrdenServicioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenActivoFijo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenActivoFijo_ActivoFijo_ActivoFijoId",
                        column: x => x.ActivoFijoId,
                        principalTable: "ActivoFijo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdenActivoFijo_OrdenServicio_OrdenServicioId",
                        column: x => x.OrdenServicioId,
                        principalTable: "OrdenServicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdenInsumo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InsumoId = table.Column<int>(nullable: true),
                    Cantidad = table.Column<int>(nullable: false),
                    Estatus = table.Column<bool>(nullable: false),
                    OrdenServicioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenInsumo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenInsumo_Insumo_InsumoId",
                        column: x => x.InsumoId,
                        principalTable: "Insumo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdenInsumo_OrdenServicio_OrdenServicioId",
                        column: x => x.OrdenServicioId,
                        principalTable: "OrdenServicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdenPersona",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonaId = table.Column<int>(nullable: true),
                    OrdenServicioId = table.Column<int>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenPersona", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenPersona_OrdenServicio_OrdenServicioId",
                        column: x => x.OrdenServicioId,
                        principalTable: "OrdenServicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdenPersona_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NuevoEstado = table.Column<string>(nullable: true),
                    ComentarioNuevoEstado = table.Column<string>(nullable: true),
                    OperadorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estado_Operador_OperadorId",
                        column: x => x.OperadorId,
                        principalTable: "Operador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Archivo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Path = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Size = table.Column<long>(nullable: false),
                    File = table.Column<byte[]>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false),
                    EstadoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Archivo_Estado_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Archivo_EstadoId",
                table: "Archivo",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_dirId",
                table: "AspNetUsers",
                column: "dirId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_perId",
                table: "AspNetUsers",
                column: "perId");

            migrationBuilder.CreateIndex(
                name: "IX_BitacoraEstatus_OsId",
                table: "BitacoraEstatus",
                column: "OsId");

            migrationBuilder.CreateIndex(
                name: "IX_BitacoraEstatus_QuienCambiaId",
                table: "BitacoraEstatus",
                column: "QuienCambiaId");

            migrationBuilder.CreateIndex(
                name: "IX_ConciliacionActivoFijo_UbicacionId",
                table: "ConciliacionActivoFijo",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConciliacionInsumo_UbicacionId",
                table: "ConciliacionInsumo",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracionServicio_ClienteId",
                table: "ConfiguracionServicio",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracionServicio_ContratoId",
                table: "ConfiguracionServicio",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracionServicio_UbicacionId",
                table: "ConfiguracionServicio",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_ClienteId",
                table: "Contrato",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_ServicioId",
                table: "Contrato",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleActivoFijo_EntradaActivoFijoId",
                table: "DetalleActivoFijo",
                column: "EntradaActivoFijoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleActivoFijo_ReferenciaId",
                table: "DetalleActivoFijo",
                column: "ReferenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleConfiguracionServicio_ConfiguracionServicioId",
                table: "DetalleConfiguracionServicio",
                column: "ConfiguracionServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleConfiguracionServicio_LineaNegocioId",
                table: "DetalleConfiguracionServicio",
                column: "LineaNegocioId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleConfiguracionServicio_ServicioId",
                table: "DetalleConfiguracionServicio",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleInsumo_EntradaInsumoId",
                table: "DetalleInsumo",
                column: "EntradaInsumoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleInsumo_ReferenciaId",
                table: "DetalleInsumo",
                column: "ReferenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_EntradaActivoFijo_UbicacionId",
                table: "EntradaActivoFijo",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_EntradaInsumo_UbicacionId",
                table: "EntradaInsumo",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Estado_OperadorId",
                table: "Estado",
                column: "OperadorId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioActivoFijo_ActivoFijoId",
                table: "InventarioActivoFijo",
                column: "ActivoFijoId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioActivoFijo_ConciliacionActivoFijoId",
                table: "InventarioActivoFijo",
                column: "ConciliacionActivoFijoId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioAF_ActivoFijoId",
                table: "InventarioAF",
                column: "ActivoFijoId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioAF_PersonaId",
                table: "InventarioAF",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioInsumo_ConciliacionInsumoId",
                table: "InventarioInsumo",
                column: "ConciliacionInsumoId");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioInsumo_InsumoId",
                table: "InventarioInsumo",
                column: "InsumoId");

            migrationBuilder.CreateIndex(
                name: "IX_Operador_OrdenServicioId",
                table: "Operador",
                column: "OrdenServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_Operador_PersonaId",
                table: "Operador",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenActivoFijo_ActivoFijoId",
                table: "OrdenActivoFijo",
                column: "ActivoFijoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenActivoFijo_OrdenServicioId",
                table: "OrdenActivoFijo",
                column: "OrdenServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenInsumo_InsumoId",
                table: "OrdenInsumo",
                column: "InsumoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenInsumo_OrdenServicioId",
                table: "OrdenInsumo",
                column: "OrdenServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenPersona_OrdenServicioId",
                table: "OrdenPersona",
                column: "OrdenServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenPersona_PersonaId",
                table: "OrdenPersona",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenServicio_ClienteId",
                table: "OrdenServicio",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenServicio_ContratoId",
                table: "OrdenServicio",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenServicio_LineaNegocioId",
                table: "OrdenServicio",
                column: "LineaNegocioId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenServicio_PersonaComercialId",
                table: "OrdenServicio",
                column: "PersonaComercialId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenServicio_PersonaValidaId",
                table: "OrdenServicio",
                column: "PersonaValidaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenServicio_ServicioId",
                table: "OrdenServicio",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenServicio_UbicacionId",
                table: "OrdenServicio",
                column: "UbicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_DirId",
                table: "Persona",
                column: "DirId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_LineaNegocioId",
                table: "Servicio",
                column: "LineaNegocioId");

            migrationBuilder.CreateIndex(
                name: "IX_TraspasoActivoFijo_UbicacionDestinoId",
                table: "TraspasoActivoFijo",
                column: "UbicacionDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_TraspasoActivoFijo_UbicacionOrigenId",
                table: "TraspasoActivoFijo",
                column: "UbicacionOrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_TraspasoDetalleActivoFijo_ActivoFijoId",
                table: "TraspasoDetalleActivoFijo",
                column: "ActivoFijoId");

            migrationBuilder.CreateIndex(
                name: "IX_TraspasoDetalleActivoFijo_TraspasoActivoFijoId",
                table: "TraspasoDetalleActivoFijo",
                column: "TraspasoActivoFijoId");

            migrationBuilder.CreateIndex(
                name: "IX_TraspasoDetalleInsumo_InsumoId",
                table: "TraspasoDetalleInsumo",
                column: "InsumoId");

            migrationBuilder.CreateIndex(
                name: "IX_TraspasoDetalleInsumo_TraspasoInsumoId",
                table: "TraspasoDetalleInsumo",
                column: "TraspasoInsumoId");

            migrationBuilder.CreateIndex(
                name: "IX_TraspasoInsumo_UbicacionDestinoId",
                table: "TraspasoInsumo",
                column: "UbicacionDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_TraspasoInsumo_UbicacionOrigenId",
                table: "TraspasoInsumo",
                column: "UbicacionOrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_Ubicacion_ClienteId",
                table: "Ubicacion",
                column: "ClienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administracion");

            migrationBuilder.DropTable(
                name: "Archivo");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Bitacora");

            migrationBuilder.DropTable(
                name: "BitacoraEstatus");

            migrationBuilder.DropTable(
                name: "Comercial");

            migrationBuilder.DropTable(
                name: "DetalleActivoFijo");

            migrationBuilder.DropTable(
                name: "DetalleConfiguracionServicio");

            migrationBuilder.DropTable(
                name: "DetalleInsumo");

            migrationBuilder.DropTable(
                name: "InventarioActivoFijo");

            migrationBuilder.DropTable(
                name: "InventarioAF");

            migrationBuilder.DropTable(
                name: "InventarioInsumo");

            migrationBuilder.DropTable(
                name: "OrdenActivoFijo");

            migrationBuilder.DropTable(
                name: "OrdenInsumo");

            migrationBuilder.DropTable(
                name: "OrdenPersona");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "TraspasoDetalleActivoFijo");

            migrationBuilder.DropTable(
                name: "TraspasoDetalleInsumo");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "EntradaActivoFijo");

            migrationBuilder.DropTable(
                name: "ConfiguracionServicio");

            migrationBuilder.DropTable(
                name: "EntradaInsumo");

            migrationBuilder.DropTable(
                name: "ConciliacionActivoFijo");

            migrationBuilder.DropTable(
                name: "ConciliacionInsumo");

            migrationBuilder.DropTable(
                name: "ActivoFijo");

            migrationBuilder.DropTable(
                name: "TraspasoActivoFijo");

            migrationBuilder.DropTable(
                name: "Insumo");

            migrationBuilder.DropTable(
                name: "TraspasoInsumo");

            migrationBuilder.DropTable(
                name: "Operador");

            migrationBuilder.DropTable(
                name: "OrdenServicio");

            migrationBuilder.DropTable(
                name: "Contrato");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "Ubicacion");

            migrationBuilder.DropTable(
                name: "Servicio");

            migrationBuilder.DropTable(
                name: "Direccion");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "LineaNegocio");
        }
    }
}
