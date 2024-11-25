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
{%- set entity_name = entity_key | snake_case %}
            migrationBuilder.CreateTable(
                name: "{{ entity_name }}",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_{{ entity_name }}", x => x.id);
                });
{% endfor %}
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
{%- for entity_key in model.entities -%}
{%- set EntityName = entity_key | pascal_case -%}
{%- set entityName = entity_key | camel_case %}
{%- set entity_name = entity_key | snake_case %}
            migrationBuilder.DropTable(
                name: "{{ entity_name }}");
{% endfor %}
        }
    }
}