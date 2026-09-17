# Semana 05 – Eliminación Lógica con Procedimientos Almacenados (C# / ExecuteNonQuery)
 
## 📌 Descripción del laboratorio
 
En este desafío se diseña e implementa un programa que interactúa con la base de datos **NeptunoDB**, aplicando el concepto de **eliminación lógica** en lugar de eliminación física (`DELETE`).
 
### Pasos realizados
 
1. Descarga del archivo `NeptunoDB`.
2. Ejecución de un script para agregar el campo de estado `Activo BIT` (valor por defecto `1`) a las tablas `Productos`, `Categorías`, `Proveedores` y `Pedidos`.
3. Creación de procedimientos almacenados **CRUD** para:
   - Productos
   - Categorías
   - Proveedores
   - Pedidos
4. En todos los casos, el procedimiento de **eliminación** actualiza `Activo = 0` en vez de ejecutar un `DELETE` físico.
5. Los procedimientos se consumen desde C# utilizando `ExecuteNonQuery` (para Insertar, Actualizar y Eliminar) y `ExecuteReader`/`DataAdapter` (para Listar/Buscar).
---
 
## 🗄️ 1. Script de modificación de tablas (campo `Activo`)
 
```sql
USE NeptunoDB;
GO
 
-- Agregar el campo Activo a Categorias
ALTER TABLE dbo.Categorias
ADD Activo BIT NOT NULL DEFAULT 1;
GO
 
-- Agregar el campo Activo a Proveedores
ALTER TABLE dbo.Proveedores
ADD Activo BIT NOT NULL DEFAULT 1;
GO
 
-- Agregar el campo Activo a Productos
ALTER TABLE dbo.Productos
ADD Activo BIT NOT NULL DEFAULT 1;
GO
 
-- Agregar el campo Activo a Pedidos
ALTER TABLE dbo.Pedidos
ADD Activo BIT NOT NULL DEFAULT 1;
GO
```
 
---
 
## 📁 2. Procedimientos almacenados – Categorías
 
```sql
USE NeptunoDB;
GO
 
-- =============================================
-- 1. LISTAR CATEGORÍAS ACTIVAS
-- =============================================
CREATE OR ALTER PROCEDURE sp_Categorias_Listar
AS
BEGIN
    SELECT
        CategoriaID,
        NombreCategoria,
        Descripcion,
        Activo
    FROM dbo.Categorias
    WHERE Activo = 1;
END;
GO
 
-- =============================================
-- 2. INSERTAR CATEGORÍA
-- =============================================
CREATE OR ALTER PROCEDURE sp_Categorias_Insertar
    @NombreCategoria NVARCHAR(30),
    @Descripcion NVARCHAR(200) = NULL
AS
BEGIN
    INSERT INTO dbo.Categorias
    (
        NombreCategoria,
        Descripcion,
        Activo
    )
    VALUES
    (
        @NombreCategoria,
        @Descripcion,
        1
    );
END;
GO
 
-- =============================================
-- 3. ACTUALIZAR CATEGORÍA
-- =============================================
CREATE OR ALTER PROCEDURE sp_Categorias_Actualizar
    @CategoriaID INT,
    @NombreCategoria NVARCHAR(30),
    @Descripcion NVARCHAR(200) = NULL
AS
BEGIN
    UPDATE dbo.Categorias
    SET
        NombreCategoria = @NombreCategoria,
        Descripcion = @Descripcion
    WHERE CategoriaID = @CategoriaID
      AND Activo = 1;
END;
GO
 
-- =============================================
-- 4. ELIMINAR CATEGORÍA (ELIMINACIÓN LÓGICA)
-- =============================================
CREATE OR ALTER PROCEDURE sp_Categorias_Eliminar
    @CategoriaID INT
AS
BEGIN
    UPDATE dbo.Categorias
    SET Activo = 0
    WHERE CategoriaID = @CategoriaID
      AND Activo = 1;
END;
GO
```
 
---
 
## 📁 3. Procedimientos almacenados – Proveedores
 
```sql
USE NeptunoDB;
GO
 
-- =============================================
-- 1. LISTAR PROVEEDORES ACTIVOS
-- =============================================
CREATE OR ALTER PROCEDURE sp_Proveedores_Listar
AS
BEGIN
    SELECT
        ProveedorID,
        CompaniaNombre,
        NombreContacto,
        CargoContacto,
        Direccion,
        Ciudad,
        CodigoPostal,
        Pais,
        Telefono,
        Fax,
        Activo
    FROM dbo.Proveedores
    WHERE Activo = 1;
END;
GO
 
-- =============================================
-- 2. INSERTAR PROVEEDOR
-- =============================================
CREATE OR ALTER PROCEDURE sp_Proveedores_Insertar
    @CompaniaNombre NVARCHAR(60),
    @NombreContacto NVARCHAR(40) = NULL,
    @CargoContacto NVARCHAR(40) = NULL,
    @Direccion NVARCHAR(80) = NULL,
    @Ciudad NVARCHAR(30) = NULL,
    @CodigoPostal NVARCHAR(10) = NULL,
    @Pais NVARCHAR(30) = NULL,
    @Telefono NVARCHAR(24) = NULL,
    @Fax NVARCHAR(24) = NULL
AS
BEGIN
    INSERT INTO dbo.Proveedores
    (
        CompaniaNombre,
        NombreContacto,
        CargoContacto,
        Direccion,
        Ciudad,
        CodigoPostal,
        Pais,
        Telefono,
        Fax,
        Activo
    )
    VALUES
    (
        @CompaniaNombre,
        @NombreContacto,
        @CargoContacto,
        @Direccion,
        @Ciudad,
        @CodigoPostal,
        @Pais,
        @Telefono,
        @Fax,
        1
    );
END;
GO
 
-- =============================================
-- 3. ACTUALIZAR PROVEEDOR
-- =============================================
CREATE OR ALTER PROCEDURE sp_Proveedores_Actualizar
    @ProveedorID INT,
    @CompaniaNombre NVARCHAR(60),
    @NombreContacto NVARCHAR(40) = NULL,
    @CargoContacto NVARCHAR(40) = NULL,
    @Direccion NVARCHAR(80) = NULL,
    @Ciudad NVARCHAR(30) = NULL,
    @CodigoPostal NVARCHAR(10) = NULL,
    @Pais NVARCHAR(30) = NULL,
    @Telefono NVARCHAR(24) = NULL,
    @Fax NVARCHAR(24) = NULL
AS
BEGIN
    UPDATE dbo.Proveedores
    SET
        CompaniaNombre = @CompaniaNombre,
        NombreContacto = @NombreContacto,
        CargoContacto = @CargoContacto,
        Direccion = @Direccion,
        Ciudad = @Ciudad,
        CodigoPostal = @CodigoPostal,
        Pais = @Pais,
        Telefono = @Telefono,
        Fax = @Fax
    WHERE ProveedorID = @ProveedorID
      AND Activo = 1;
END;
GO
 
-- =============================================
-- 4. ELIMINAR PROVEEDOR (ELIMINACIÓN LÓGICA)
-- =============================================
CREATE OR ALTER PROCEDURE sp_Proveedores_Eliminar
    @ProveedorID INT
AS
BEGIN
    UPDATE dbo.Proveedores
    SET Activo = 0
    WHERE ProveedorID = @ProveedorID
      AND Activo = 1;
END;
GO
 
-- =============================================
-- 5. BUSCAR PROVEEDORES (por contacto y/o ciudad)
-- =============================================
CREATE OR ALTER PROCEDURE sp_Proveedores_Buscar
    @NombreContacto NVARCHAR(40) = NULL,
    @Ciudad NVARCHAR(30) = NULL
AS
BEGIN
    SELECT
        ProveedorID,
        CompaniaNombre,
        NombreContacto,
        CargoContacto,
        Direccion,
        Ciudad,
        CodigoPostal,
        Pais,
        Telefono,
        Fax,
        Activo
    FROM dbo.Proveedores
    WHERE Activo = 1
      AND (@NombreContacto IS NULL 
           OR NombreContacto LIKE N'%' + @NombreContacto + N'%')
      AND (@Ciudad IS NULL 
           OR Ciudad LIKE N'%' + @Ciudad + N'%');
END;
GO
```
 
---
 
## 📁 4. Procedimientos almacenados – Productos
 
```sql
USE NeptunoDB;
GO
 
-- =============================================
-- 1. LISTAR PRODUCTOS ACTIVOS
-- =============================================
CREATE OR ALTER PROCEDURE sp_Productos_Listar
AS
BEGIN
    SELECT 
        ProductoID,
        NombreProducto,
        ProveedorID,
        CategoriaID,
        CantidadPorUnidad,
        PrecioUnidad,
        UnidadesEnExistencia,
        UnidadesEnPedido,
        NivelDeReorden,
        Descontinuado,
        Activo
    FROM dbo.Productos
    WHERE Activo = 1;
END;
GO
 
-- =============================================
-- 2. INSERTAR PRODUCTO
-- =============================================
CREATE OR ALTER PROCEDURE sp_Productos_Insertar
    @NombreProducto NVARCHAR(60),
    @ProveedorID INT = NULL,
    @CategoriaID INT = NULL,
    @CantidadPorUnidad NVARCHAR(30) = NULL,
    @PrecioUnidad DECIMAL(10,2),
    @UnidadesEnExistencia SMALLINT,
    @UnidadesEnPedido SMALLINT,
    @NivelDeReorden SMALLINT,
    @Descontinuado BIT
AS
BEGIN
    INSERT INTO dbo.Productos
    (
        NombreProducto,
        ProveedorID,
        CategoriaID,
        CantidadPorUnidad,
        PrecioUnidad,
        UnidadesEnExistencia,
        UnidadesEnPedido,
        NivelDeReorden,
        Descontinuado,
        Activo
    )
    VALUES
    (
        @NombreProducto,
        @ProveedorID,
        @CategoriaID,
        @CantidadPorUnidad,
        @PrecioUnidad,
        @UnidadesEnExistencia,
        @UnidadesEnPedido,
        @NivelDeReorden,
        @Descontinuado,
        1
    );
END;
GO
 
-- =============================================
-- 3. ACTUALIZAR PRODUCTO
-- =============================================
CREATE OR ALTER PROCEDURE sp_Productos_Actualizar
    @ProductoID INT,
    @NombreProducto NVARCHAR(60),
    @ProveedorID INT = NULL,
    @CategoriaID INT = NULL,
    @CantidadPorUnidad NVARCHAR(30) = NULL,
    @PrecioUnidad DECIMAL(10,2),
    @UnidadesEnExistencia SMALLINT,
    @UnidadesEnPedido SMALLINT,
    @NivelDeReorden SMALLINT,
    @Descontinuado BIT
AS
BEGIN
    UPDATE dbo.Productos
    SET
        NombreProducto = @NombreProducto,
        ProveedorID = @ProveedorID,
        CategoriaID = @CategoriaID,
        CantidadPorUnidad = @CantidadPorUnidad,
        PrecioUnidad = @PrecioUnidad,
        UnidadesEnExistencia = @UnidadesEnExistencia,
        UnidadesEnPedido = @UnidadesEnPedido,
        NivelDeReorden = @NivelDeReorden,
        Descontinuado = @Descontinuado
    WHERE ProductoID = @ProductoID
      AND Activo = 1;
END;
GO
 
-- =============================================
-- 4. ELIMINAR PRODUCTO (ELIMINACIÓN LÓGICA)
-- =============================================
CREATE OR ALTER PROCEDURE sp_Productos_Eliminar
    @ProductoID INT
AS
BEGIN
    UPDATE dbo.Productos
    SET Activo = 0
    WHERE ProductoID = @ProductoID
      AND Activo = 1;
END;
GO
```
 
---
 
## 📁 5. Procedimientos almacenados – Pedidos
 
```sql
USE NeptunoDB;
GO
 
-- =============================================
-- 1. LISTAR PEDIDOS ACTIVOS
-- =============================================
CREATE OR ALTER PROCEDURE sp_Pedidos_Listar
AS
BEGIN
    SELECT
        PedidoID,
        ClienteID,
        EmpleadoID,
        FechaPedido,
        FechaRequerida,
        FechaEnvio,
        TransportistaID,
        Destinatario,
        CiudadDestino,
        PaisDestino,
        Activo
    FROM dbo.Pedidos
    WHERE Activo = 1;
END;
GO
 
-- =============================================
-- 2. INSERTAR PEDIDO
-- =============================================
CREATE OR ALTER PROCEDURE sp_Pedidos_Insertar
    @ClienteID INT = NULL,
    @EmpleadoID INT = NULL,
    @FechaPedido DATE,
    @FechaRequerida DATE = NULL,
    @FechaEnvio DATE = NULL,
    @TransportistaID INT = NULL,
    @Destinatario NVARCHAR(60) = NULL,
    @CiudadDestino NVARCHAR(30) = NULL,
    @PaisDestino NVARCHAR(30) = NULL
AS
BEGIN
    INSERT INTO dbo.Pedidos
    (
        ClienteID,
        EmpleadoID,
        FechaPedido,
        FechaRequerida,
        FechaEnvio,
        TransportistaID,
        Destinatario,
        CiudadDestino,
        PaisDestino,
        Activo
    )
    VALUES
    (
        @ClienteID,
        @EmpleadoID,
        @FechaPedido,
        @FechaRequerida,
        @FechaEnvio,
        @TransportistaID,
        @Destinatario,
        @CiudadDestino,
        @PaisDestino,
        1
    );
END;
GO
 
-- =============================================
-- 3. ACTUALIZAR PEDIDO
-- =============================================
CREATE OR ALTER PROCEDURE sp_Pedidos_Actualizar
    @PedidoID INT,
    @ClienteID INT = NULL,
    @EmpleadoID INT = NULL,
    @FechaPedido DATE,
    @FechaRequerida DATE = NULL,
    @FechaEnvio DATE = NULL,
    @TransportistaID INT = NULL,
    @Destinatario NVARCHAR(60) = NULL,
    @CiudadDestino NVARCHAR(30) = NULL,
    @PaisDestino NVARCHAR(30) = NULL
AS
BEGIN
    UPDATE dbo.Pedidos
    SET
        ClienteID = @ClienteID,
        EmpleadoID = @EmpleadoID,
        FechaPedido = @FechaPedido,
        FechaRequerida = @FechaRequerida,
        FechaEnvio = @FechaEnvio,
        TransportistaID = @TransportistaID,
        Destinatario = @Destinatario,
        CiudadDestino = @CiudadDestino,
        PaisDestino = @PaisDestino
    WHERE PedidoID = @PedidoID
      AND Activo = 1;
END;
GO
 
-- =============================================
-- 4. ELIMINAR PEDIDO (ELIMINACIÓN LÓGICA)
-- =============================================
CREATE OR ALTER PROCEDURE sp_Pedidos_Eliminar
    @PedidoID INT
AS
BEGIN
    UPDATE dbo.Pedidos
    SET Activo = 0
    WHERE PedidoID = @PedidoID
      AND Activo = 1;
END;
GO
 
-- =============================================
-- 5. DETALLE DE PEDIDOS POR RANGO DE FECHAS
-- =============================================
CREATE OR ALTER PROCEDURE sp_DetallePedidos_PorFecha
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    SELECT
        p.PedidoID,
        p.FechaPedido,
        p.FechaRequerida,
        p.FechaEnvio,
        p.Destinatario,
        p.CiudadDestino,
        p.PaisDestino,
 
        dp.ProductoID,
        pr.NombreProducto,
        dp.PrecioUnidad,
        dp.Cantidad,
        dp.Descuento
 
    FROM dbo.Pedidos p
 
    INNER JOIN dbo.DetallePedidos dp
        ON p.PedidoID = dp.PedidoID
 
    INNER JOIN dbo.Productos pr
        ON dp.ProductoID = pr.ProductoID
 
    WHERE p.FechaPedido BETWEEN @FechaInicio AND @FechaFin
      AND p.Activo = 1;
END;
GO
```
 
---
 
## ⚙️ Notas de implementación (C#)
 
- Los procedimientos de **Insertar**, **Actualizar** y **Eliminar** se ejecutan desde C# con `ExecuteNonQuery`.
- Los procedimientos de **Listar** y **Buscar** se ejecutan con `ExecuteReader` o un `DataAdapter`, ya que retornan un conjunto de resultados.
- La eliminación siempre es **lógica**: se actualiza el campo `Activo` a `0` en lugar de borrar el registro físicamente, preservando así la integridad histórica de la información (útil por ejemplo para no perder relaciones con `Pedidos` o `DetallePedidos`).
