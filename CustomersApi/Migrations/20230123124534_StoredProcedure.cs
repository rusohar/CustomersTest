using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomersApi.Migrations
{
    /// <inheritdoc />
    public partial class StoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[spExtendedSearch] 
	                    
	                        @Name nvarchar(50) =NULL,   
                            @CompanyName nvarchar(50) = NULL,
	                        @Phone nvarchar(20) = NULL,   
                            @Email nvarchar(50)= NULL,
	                        @SortOrder varchar(4)=NULL,
	                        @SortColumn INT = 1,
	                        @PageNo INT = 1,
                            @PageSize INT = 20
                        AS
                        BEGIN
	                        
                            SET @PageNo = COALESCE(
                                    CASE 
                                         WHEN @PageNo < 1 THEN 1
                                         ELSE @PageNo
                                    END, 1)

		                    SET @PageSize = COALESCE(
                                    CASE 
                                         WHEN @PageSize < 1 THEN 20
                                         ELSE @PageSize
                            END, 20)

                            SET @SortColumn = COALESCE(
                                    CASE 
                                         WHEN @SortColumn < 1 THEN 1
                                         ELSE @SortColumn
                                    END, 1)

	                        SELECT Id, Name,CompanyName,Phone,Email FROM Customers
	                        WHERE 1 = 1
		                        AND (@Name IS NULL OR Name Like '%'+@Name+'%' )
		                        AND (@CompanyName IS NULL OR @CompanyName Like '%'+@CompanyName+'%')
		                        AND (@Phone IS NULL OR Phone Like '%'+@Phone+'%')
		                        AND (@Email IS NULL OR Email Like '%'+@Email+'%')
	                        ORDER BY 
		                        CASE WHEN  @SortColumn=1 AND @SortOrder='asc'  THEN Name END ASC,
		                        CASE WHEN  @SortColumn=2 AND @SortOrder='asc'  THEN CompanyName END ASC,
		                        CASE WHEN  @SortColumn=3 AND @SortOrder='asc'  THEN Phone END ASC,
		                        CASE WHEN  @SortColumn=4 AND @SortOrder='asc'  THEN Email END ASC,
				                        
		                        CASE WHEN  @SortColumn=1 AND @SortOrder='desc'  THEN Name END DESC,
		                        CASE WHEN  @SortColumn=2 AND @SortOrder='desc'  THEN CompanyName END DESC,
		                        CASE WHEN  @SortColumn=3 AND @SortOrder='desc'  THEN Phone END DESC,
		                        CASE WHEN  @SortColumn=4 AND @SortOrder='desc'  THEN Email END DESC

	                        OFFSET @PageSize * (@PageNo - 1) ROWS
                                    FETCH NEXT @PageSize ROWS ONLY

                        END";

            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
