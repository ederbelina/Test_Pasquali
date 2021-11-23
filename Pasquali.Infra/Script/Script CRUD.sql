/*tabelas*/
CREATE TABLE Usuario
(  
	UsuarioId       Int Identity(1, 1) not null,
	Nome            Varchar(100),
	Login           Varchar(50),
	Senha           Varchar(50),
	IsAdmin         Bit
);
GO

CREATE TABLE LogAcesso  
(  
	LogAcessoId     Int Identity(1, 1) not null,
	UsuarioId       Int,
	DataHoraAcesso  DateTime,
	EnderecoIp      Varchar(50)
);
GO

/*Procs*/
Create Proc AdicionarUsuario
	@Nome Varchar(100),
	@Login Varchar(50),
	@Senha Varchar(50),
	@IsAdmin Bit

As 
	INSERT INTO Usuario (Nome, Login, Senha, IsAdmin) values (@Nome, @Login, @Senha, @IsAdmin)
GO

CREATE Proc ObterTodosUsuario
As

	Select 
		UsuarioId, 
		Nome, 
		[Login], 
		Senha, 
		IsAdmin 
	FROM 
		Usuario
GO

CREATE Proc ObterUsuario
	@Login Varchar(50),
	@Senha Varchar(50)
As

	Select 
		UsuarioId, 
		Nome, 
		[Login], 
		Senha, 
		IsAdmin 
	FROM 
		Usuario 
	WHERE 
		[Login] = @Login
		and Senha = @Senha
GO

CREATE Proc ObterUsuarioPorId
	@UsuarioId int
As

	Select 
		UsuarioId, 
		Nome, 
		[Login], 
		Senha, 
		IsAdmin 
	FROM 
		Usuario 
	WHERE 
		UsuarioId = @UsuarioId
Go
