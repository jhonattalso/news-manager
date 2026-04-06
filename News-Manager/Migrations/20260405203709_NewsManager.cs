using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace News_Manager.Migrations
{
    /// <inheritdoc />
    public partial class NewsManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_NEWS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DS_TITULO = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    NM_AUTOR = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DT_PUBLICACAO = table.Column<DateTime>(type: "DATE", nullable: false),
                    NR_CATEGORIA = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TX_CONTEUDO = table.Column<string>(type: "CLOB", nullable: false),
                    ST_PUBLICADO = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_NEWS", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_NEWS");
        }
    }
}
