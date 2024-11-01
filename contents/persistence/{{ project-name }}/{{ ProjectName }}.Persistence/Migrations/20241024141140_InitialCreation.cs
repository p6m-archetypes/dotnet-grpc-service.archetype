using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace {{ ProjectName }}.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
{%- for entity_key in model.entities -%}
{%- set EntityName = entity_key | pascal_case -%}
{%- set entityName = entity_key | camel_case %}
            migrationBuilder.CreateTable(
                name: "{{ entityName }}",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_{{ entityName }}", x => x.id);
                });
{% endfor %}
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
{%- for entity_key in model.entities -%}
{%- set EntityName = entity_key | pascal_case -%}
{%- set entityName = entity_key | camel_case %}
            migrationBuilder.DropTable(
                name: "{{ entityName }}");
{% endfor %}
        }
    }
}