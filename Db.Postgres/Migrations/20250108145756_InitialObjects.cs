using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Juliapos.Portal.ProductApi.Db.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class InitialObjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "product");

            migrationBuilder.CreateTable(
                name: "dustcategory",
                schema: "product",
                columns: table => new
                {
                    dustcategory_id = table.Column<Guid>(type: "uuid", nullable: false),
                    organization_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    weight = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dustcategory", x => x.dustcategory_id);
                });

            migrationBuilder.CreateTable(
                name: "menucategory",
                schema: "product",
                columns: table => new
                {
                    menucategory_id = table.Column<Guid>(type: "uuid", nullable: false),
                    organization_id = table.Column<Guid>(type: "uuid", nullable: false),
                    idname = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    weight = table.Column<int>(type: "integer", nullable: false),
                    enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menucategory", x => x.menucategory_id);
                });

            migrationBuilder.CreateTable(
                name: "organization",
                schema: "product",
                columns: table => new
                {
                    organization_id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: true),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization", x => x.organization_id);
                });

            migrationBuilder.CreateTable(
                name: "property",
                schema: "product",
                columns: table => new
                {
                    property_id = table.Column<Guid>(type: "uuid", nullable: false),
                    organization_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    idname = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    typename = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_property", x => x.property_id);
                });

            migrationBuilder.CreateTable(
                name: "selectionpage",
                schema: "product",
                columns: table => new
                {
                    selectionpage_id = table.Column<Guid>(type: "uuid", nullable: false),
                    organization_id = table.Column<Guid>(type: "uuid", nullable: false),
                    idname = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    weight = table.Column<int>(type: "integer", nullable: false),
                    enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_selectionpage", x => x.selectionpage_id);
                });

            migrationBuilder.CreateTable(
                name: "location",
                schema: "product",
                columns: table => new
                {
                    location_id = table.Column<Guid>(type: "uuid", nullable: false),
                    organization_id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_location", x => x.location_id);
                    table.ForeignKey(
                        name: "FK_location_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "product",
                        principalTable: "organization",
                        principalColumn: "organization_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productcategory",
                schema: "product",
                columns: table => new
                {
                    productcategory_id = table.Column<Guid>(type: "uuid", nullable: false),
                    organization_id = table.Column<Guid>(type: "uuid", nullable: false),
                    idname = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    weight = table.Column<int>(type: "integer", nullable: false),
                    measuremethod = table.Column<int>(type: "integer", nullable: false),
                    forecolor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    backcolor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productcategory", x => x.productcategory_id);
                    table.ForeignKey(
                        name: "FK_productcategory_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "product",
                        principalTable: "organization",
                        principalColumn: "organization_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "menucategoryproperty",
                schema: "product",
                columns: table => new
                {
                    menucategory_id = table.Column<Guid>(type: "uuid", nullable: false),
                    property_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menucategoryproperty", x => new { x.menucategory_id, x.property_id });
                    table.ForeignKey(
                        name: "FK_menucategoryproperty_menucategory_menucategory_id",
                        column: x => x.menucategory_id,
                        principalSchema: "product",
                        principalTable: "menucategory",
                        principalColumn: "menucategory_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_menucategoryproperty_property_property_id",
                        column: x => x.property_id,
                        principalSchema: "product",
                        principalTable: "property",
                        principalColumn: "property_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product",
                schema: "product",
                columns: table => new
                {
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    dustcategory_id = table.Column<Guid>(type: "uuid", nullable: false),
                    menucategory_id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    menuname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    vatlevel = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    ininventory = table.Column<bool>(type: "boolean", nullable: false),
                    percentage = table.Column<float>(type: "real", nullable: false),
                    ascendingstock = table.Column<bool>(type: "boolean", nullable: false),
                    state = table.Column<int>(type: "integer", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    usercreate = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    userupdate = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.product_id);
                    table.ForeignKey(
                        name: "FK_product_dustcategory_dustcategory_id",
                        column: x => x.dustcategory_id,
                        principalSchema: "product",
                        principalTable: "dustcategory",
                        principalColumn: "dustcategory_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_menucategory_menucategory_id",
                        column: x => x.menucategory_id,
                        principalSchema: "product",
                        principalTable: "menucategory",
                        principalColumn: "menucategory_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_product_productcategory_category_id",
                        column: x => x.category_id,
                        principalSchema: "product",
                        principalTable: "productcategory",
                        principalColumn: "productcategory_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productvariation",
                schema: "product",
                columns: table => new
                {
                    productvariation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sku = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productvariation", x => x.productvariation_id);
                    table.ForeignKey(
                        name: "FK_productvariation_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "product",
                        principalTable: "product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "propertyvalue",
                schema: "product",
                columns: table => new
                {
                    propertyvalue_id = table.Column<Guid>(type: "uuid", nullable: false),
                    property_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_propertyvalue", x => x.propertyvalue_id);
                    table.ForeignKey(
                        name: "FK_propertyvalue_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "product",
                        principalTable: "product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_propertyvalue_property_property_id",
                        column: x => x.property_id,
                        principalSchema: "product",
                        principalTable: "property",
                        principalColumn: "property_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "selectionpageproduct",
                schema: "product",
                columns: table => new
                {
                    selectionpageproduct_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    selectionpage_id = table.Column<Guid>(type: "uuid", nullable: false),
                    forecolor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    backcolor = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    row = table.Column<int>(type: "integer", nullable: false),
                    column = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_selectionpageproduct", x => x.selectionpageproduct_id);
                    table.ForeignKey(
                        name: "FK_selectionpageproduct_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "product",
                        principalTable: "product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_selectionpageproduct_selectionpage_selectionpage_id",
                        column: x => x.selectionpage_id,
                        principalSchema: "product",
                        principalTable: "selectionpage",
                        principalColumn: "selectionpage_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productvariationlocation",
                schema: "product",
                columns: table => new
                {
                    productvariationlocation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    productvariation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    location_id = table.Column<Guid>(type: "uuid", nullable: false),
                    unitprice = table.Column<float>(type: "real", nullable: true),
                    unitpricepurchase = table.Column<float>(type: "real", nullable: true),
                    favorite = table.Column<bool>(type: "boolean", nullable: true),
                    minamount = table.Column<float>(type: "real", nullable: true),
                    maxamount = table.Column<float>(type: "real", nullable: true),
                    transport = table.Column<float>(type: "real", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    nextstatus = table.Column<int>(type: "integer", nullable: true),
                    changedatetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    onmenustart = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    onmenuend = table.Column<TimeOnly>(type: "time without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productvariationlocation", x => x.productvariationlocation_id);
                    table.ForeignKey(
                        name: "FK_productvariationlocation_location_location_id",
                        column: x => x.location_id,
                        principalSchema: "product",
                        principalTable: "location",
                        principalColumn: "location_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productvariationlocation_productvariation_productvariation_~",
                        column: x => x.productvariation_id,
                        principalSchema: "product",
                        principalTable: "productvariation",
                        principalColumn: "productvariation_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dustcategory_organization_id_name",
                schema: "product",
                table: "dustcategory",
                columns: new[] { "organization_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_location_organization_id_name",
                schema: "product",
                table: "location",
                columns: new[] { "organization_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_menucategory_organization_id_name",
                schema: "product",
                table: "menucategory",
                columns: new[] { "organization_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_menucategoryproperty_property_id",
                schema: "product",
                table: "menucategoryproperty",
                column: "property_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_category_id",
                schema: "product",
                table: "product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_dustcategory_id",
                schema: "product",
                table: "product",
                column: "dustcategory_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_menucategory_id",
                schema: "product",
                table: "product",
                column: "menucategory_id");

            migrationBuilder.CreateIndex(
                name: "IX_productcategory_organization_id_name",
                schema: "product",
                table: "productcategory",
                columns: new[] { "organization_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_productvariation_product_id",
                schema: "product",
                table: "productvariation",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_productvariationlocation_location_id",
                schema: "product",
                table: "productvariationlocation",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_productvariationlocation_productvariation_id",
                schema: "product",
                table: "productvariationlocation",
                column: "productvariation_id");

            migrationBuilder.CreateIndex(
                name: "IX_property_organization_id_name",
                schema: "product",
                table: "property",
                columns: new[] { "organization_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_propertyvalue_product_id",
                schema: "product",
                table: "propertyvalue",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_propertyvalue_property_id",
                schema: "product",
                table: "propertyvalue",
                column: "property_id");

            migrationBuilder.CreateIndex(
                name: "IX_selectionpage_organization_id_name",
                schema: "product",
                table: "selectionpage",
                columns: new[] { "organization_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_selectionpageproduct_product_id",
                schema: "product",
                table: "selectionpageproduct",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_selectionpageproduct_selectionpage_id",
                schema: "product",
                table: "selectionpageproduct",
                column: "selectionpage_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "menucategoryproperty",
                schema: "product");

            migrationBuilder.DropTable(
                name: "productvariationlocation",
                schema: "product");

            migrationBuilder.DropTable(
                name: "propertyvalue",
                schema: "product");

            migrationBuilder.DropTable(
                name: "selectionpageproduct",
                schema: "product");

            migrationBuilder.DropTable(
                name: "location",
                schema: "product");

            migrationBuilder.DropTable(
                name: "productvariation",
                schema: "product");

            migrationBuilder.DropTable(
                name: "property",
                schema: "product");

            migrationBuilder.DropTable(
                name: "selectionpage",
                schema: "product");

            migrationBuilder.DropTable(
                name: "product",
                schema: "product");

            migrationBuilder.DropTable(
                name: "dustcategory",
                schema: "product");

            migrationBuilder.DropTable(
                name: "menucategory",
                schema: "product");

            migrationBuilder.DropTable(
                name: "productcategory",
                schema: "product");

            migrationBuilder.DropTable(
                name: "organization",
                schema: "product");
        }
    }
}
