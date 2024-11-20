using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GsDotNet.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USUARIO_ENERGIA",
                columns: table => new
                {
                    ID_USUARIO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    SENHA = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_ENERGIA", x => x.ID_USUARIO);
                });

            migrationBuilder.CreateTable(
                name: "CONSUMO_ENERGIA",
                columns: table => new
                {
                    ID_CONSUMO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_USUARIO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DATA_REGISTRO = table.Column<DateTime>(type: "DATE", nullable: false),
                    CONSUMO_KWH = table.Column<decimal>(type: "NUMBER(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONSUMO_ENERGIA", x => x.ID_CONSUMO);
                    table.ForeignKey(
                        name: "FK_CONSUMO_ENERGIA_USUARIO_ENERGIA_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO_ENERGIA",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FEEDBACK_CONSUMO",
                columns: table => new
                {
                    ID_FEEDBACK = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_USUARIO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    MENSAGEM_FEEDBACK = table.Column<string>(type: "NVARCHAR2(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FEEDBACK_CONSUMO", x => x.ID_FEEDBACK);
                    table.ForeignKey(
                        name: "FK_FEEDBACK_CONSUMO_USUARIO_ENERGIA_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO_ENERGIA",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HISTORICO_CONSUMO",
                columns: table => new
                {
                    ID_HISTORICO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_USUARIO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TOTAL_CONSUMO = table.Column<decimal>(type: "NUMBER(10,2)", nullable: false),
                    MEDIA_MENSAL = table.Column<decimal>(type: "NUMBER(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HISTORICO_CONSUMO", x => x.ID_HISTORICO);
                    table.ForeignKey(
                        name: "FK_HISTORICO_CONSUMO_USUARIO_ENERGIA_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO_ENERGIA",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CONSUMO_ENERGIA_ID_USUARIO",
                table: "CONSUMO_ENERGIA",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_FEEDBACK_CONSUMO_ID_USUARIO",
                table: "FEEDBACK_CONSUMO",
                column: "ID_USUARIO");

            migrationBuilder.CreateIndex(
                name: "IX_HISTORICO_CONSUMO_ID_USUARIO",
                table: "HISTORICO_CONSUMO",
                column: "ID_USUARIO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CONSUMO_ENERGIA");

            migrationBuilder.DropTable(
                name: "FEEDBACK_CONSUMO");

            migrationBuilder.DropTable(
                name: "HISTORICO_CONSUMO");

            migrationBuilder.DropTable(
                name: "USUARIO_ENERGIA");
        }
    }
}
