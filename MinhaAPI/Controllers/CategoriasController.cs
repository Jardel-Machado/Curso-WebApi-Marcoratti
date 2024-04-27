using APICatalogo.Context;
using APICatalogo.DTO;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Models.Filtros;
using APICatalogo.Paginacao;
using APICatalogo.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using X.PagedList;


namespace APICatalogo.Controllers;
[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{    
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<CategoriasController> logger;
    private readonly IMapper mapper;

    public CategoriasController(ILogger<CategoriasController> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.logger = logger;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    
    [HttpGet("paginanacao")]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> BuscarCategoriasPaginadas([FromQuery] ParametrosDaPaginacao parametrosDaPaginacao)
    {
        var categorias = await unitOfWork.CategoriasRepository.BuscarCategoriasPaginadas(parametrosDaPaginacao);

        return ObterCategoriasFiltradas(categorias);
    }

    [HttpGet("filtro/nome")]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> FiltrarCategoriasPorNome([FromQuery] CategoriasListarFiltro categoriasListarFiltro)
    {
        var categoriasFiltradas = await unitOfWork.CategoriasRepository.FiltrarCategoriasPorNome(categoriasListarFiltro);

        return ObterCategoriasFiltradas(categoriasFiltradas);
    }

    private ActionResult<IEnumerable<CategoriaDTO>> ObterCategoriasFiltradas (IPagedList<Categoria> categorias)
    {
        //var metadata = new
        //{
        //    categorias.QuantidadeTotalDeItens,
        //    categorias.ItensPorPagina,
        //    categorias.PaginaAtual,
        //    categorias.TotalDePaginas,
        //    categorias.ExistePaginaSeguinte,
        //    categorias.ExistePaginaAnterior
        //};

        var metadata = new
        {
            categorias.Count,
            categorias.PageSize,
            categorias.PageCount,
            categorias.TotalItemCount,
            categorias.HasNextPage,
            categorias.HasPreviousPage
        };

        Response.Headers.Append("Paginacao", JsonConvert.SerializeObject(metadata));

        var categoriasDTO = mapper.Map<IEnumerable<CategoriaDTO>>(categorias);

        return Ok(categoriasDTO);
    }

    [HttpGet("produtos")]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> BuscarCategoriasPorProduto()
    {
       
        logger.LogInformation("=========== Get/Categorias/produtos===============");

        IEnumerable<Categoria> categoriasProdutos = await unitOfWork.CategoriasRepository.BuscarCategoriasPorProduto(); 

        var categoriasProdutosDTO = mapper.Map<IEnumerable<CategoriaDTO>>(categoriasProdutos);

        return Ok(categoriasProdutosDTO);       
    }

    [HttpGet]    
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> BuscarCategorias()
    {
        IEnumerable<Categoria> categorias = await unitOfWork.CategoriasRepository.BuscarTodos();       

        if (!categorias.Any()) 
        {
            return NotFound("Não existem categorias!");
        }

        var categoriasProdutosDTO = mapper.Map<IEnumerable<CategoriaDTO>>(categorias);

        //List<CategoriaDTO> categoriasDTO = new();

        //foreach (var categoria in categorias)
        //{
        //    CategoriaDTO categoriaDTO = new()
        //    {
        //        CategoriaId = categoria.CategoriaId,
        //        Nome = categoria.Nome,
        //        ImagemUrl = categoria.ImagemUrl
        //    };

        //    categoriasDTO.Add(categoriaDTO);
        //}

        return Ok(categoriasProdutosDTO);
       
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoriaDTO>> BuscarCategoriaPorId(int id)
    {
        Categoria categoria = await unitOfWork.CategoriasRepository.Buscar(x => x.CategoriaId == id);

        if (categoria is null)
        {
            return NotFound("Categoria não encontrada");
        }

        var categoriaDTO = mapper.Map<CategoriaDTO>(categoria);

        return Ok(categoriaDTO);        
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaDTO>> CriarCategoria(CategoriaDTO categoriaDTO)
    {
        if (categoriaDTO is null)
        {
            logger.LogWarning("Dados inválidos");

            return BadRequest("Dados inválidos");
        }

        //Categoria categoria = new()
        //{
        //    CategoriaId = categoriaDTO.CategoriaId,
        //    Nome = categoriaDTO.Nome,
        //    ImagemUrl = categoriaDTO.ImagemUrl
        //};

        var categoria = mapper.Map<Categoria>(categoriaDTO);

        Categoria categoriaCriada = unitOfWork.CategoriasRepository.Criar(categoria);

        await unitOfWork.Commit();

        //CategoriaDTO novaCategoriaDTO = new()
        //{
        //    CategoriaId = categoriaCriada.CategoriaId,
        //    Nome = categoriaCriada.Nome,
        //    ImagemUrl = categoriaCriada.ImagemUrl
        //};

        var novaCategoriaDTO = mapper.Map<CategoriaDTO>(categoriaCriada);

        return Ok(novaCategoriaDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CategoriaDTO>> EditarCategoria(int id, CategoriaDTO categoriaDTO)
    {
        if(id != categoriaDTO.CategoriaId)
        {
            logger.LogWarning("Dados inválidos");

            return BadRequest("Dados inválidos");
        }

        var categoria = mapper.Map<Categoria>(categoriaDTO);

        Categoria categoriaModificada = unitOfWork.CategoriasRepository.Editar(categoria);

        await unitOfWork.Commit();

        var categoriaDTOModificada = mapper.Map<CategoriaDTO>(categoriaModificada);

        return Ok(categoriaDTOModificada);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CategoriaDTO>> DeletarCategoria(int id)
    {
        Categoria categoria = await unitOfWork.CategoriasRepository.Buscar(x => x.CategoriaId == id);

        if (categoria is null)
        {
            logger.LogWarning($"Categoria com id {id} não encontrada");

            return NotFound("Categoria não encontrada");
        }

        Categoria categoriaExcluida = unitOfWork.CategoriasRepository.Deletar(categoria);

        await unitOfWork.Commit();

        var categoriaDTOExcluida = mapper.Map<CategoriaDTO>(categoriaExcluida);

        return Ok(categoriaDTOExcluida);
    }
}
