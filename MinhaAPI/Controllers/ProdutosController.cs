using APICatalogo.DTO;
using APICatalogo.Models;
using APICatalogo.Models.Filtros;
using APICatalogo.Paginacao;
using APICatalogo.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using X.PagedList;


namespace APICatalogo.Controllers;
[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public ProdutosController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    
    [HttpGet("paginanacao")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> BuscarProdutosPaginados([FromQuery] ParametrosDaPaginacao parametrosDaPaginacao)
    {
        var produtos = await unitOfWork.ProdutosRepository.BuscarProdutosPaginados(parametrosDaPaginacao);
        
        return ObterProdutosPaginados(produtos);
    }

    [HttpGet("filtro/preco")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> FiltrarProdutosPeloPreco([FromQuery] ProdutosListarFiltro produtosListarFiltro)
    {
        var produtos = await unitOfWork.ProdutosRepository.FiltrarProdutosPeloPreco(produtosListarFiltro);

        return ObterProdutosPaginados(produtos);
    }

    private ActionResult<IEnumerable<ProdutoDTO>> ObterProdutosPaginados(IPagedList<Produto> produtos)
    {
        //var metadata = new
        //{
        //    produtos.QuantidadeTotalDeItens,
        //    produtos.ItensPorPagina,
        //    produtos.PaginaAtual,
        //    produtos.TotalDePaginas,
        //    produtos.ExistePaginaSeguinte,
        //    produtos.ExistePaginaAnterior
        //};

        var metadata = new
        {
            produtos.Count,
            produtos.PageSize,
            produtos.PageCount,
            produtos.TotalItemCount,
            produtos.HasNextPage,
            produtos.HasPreviousPage
        };

        Response.Headers.Append("Paginacao", JsonConvert.SerializeObject(metadata));

        var produtosDTO = mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        return Ok(produtosDTO);
    }

    [HttpGet("produtos/{id}")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> BuscarProdutosPorCategoria(int id)
    {
        var produtos = await unitOfWork.ProdutosRepository.BuscarProdutoPorCategorias(id);

        if (produtos is null)
        {
            return NotFound("Produto não encontrado");
        }

        var produtosDTO = mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        return Ok(produtosDTO);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> BuscarProdutos()
    {
        var produtos = await unitOfWork.ProdutosRepository.BuscarTodos();

        if (produtos is null)
        {
            return NotFound();
        }

        var produtosDTO = mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        return Ok(produtosDTO);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProdutoDTO>> BuscarProdutoPorId(int id) 
    {
        Produto produto = await unitOfWork.ProdutosRepository.Buscar(x => x.ProdutoId == id);

        if(produto is null) 
        {
            return NotFound("Produto não encontrado");
        }

        var produtoDTO = mapper.Map<ProdutoDTO>(produto);

        return Ok(produtoDTO);
    }   

    [HttpPost]
    public async Task<ActionResult<ProdutoDTO>> CriarProduto(ProdutoDTO produtoDTO)
    {
        if (produtoDTO == null)
        {
            return BadRequest();
        }

        var produto = mapper.Map<Produto>(produtoDTO);

        Produto novoProduto = unitOfWork.ProdutosRepository.Criar(produto);

        await unitOfWork.Commit();

        var novoProdutoDTO = mapper.Map<ProdutoDTO>(novoProduto);

        string uri = Url.Action(nameof(BuscarProdutoPorId), new { id = novoProdutoDTO.ProdutoId });

        return Created(uri, novoProdutoDTO);
    }

    [HttpPatch("{id}/edicao-parcial")]
    public async Task<ActionResult<ProdutoEditarResponse>> EditarProdutoParcial(int id, JsonPatchDocument<ProdutoEditarRequest> produtoEditarRequest)
    {
        if(produtoEditarRequest is null || id <= 0)
            return BadRequest();

        var produto = await unitOfWork.ProdutosRepository.Buscar(x => x.ProdutoId == id);

        if (produto is null)
            return NotFound();

        var produtoRequest = mapper.Map<ProdutoEditarRequest>(produto);

        produtoEditarRequest.ApplyTo(produtoRequest, ModelState);

        if(!ModelState.IsValid || TryValidateModel(produtoRequest))        
            return BadRequest(ModelState);

        mapper.Map(produtoRequest, produto);

        unitOfWork.ProdutosRepository.Editar(produto);

        await unitOfWork.Commit();        

        return Ok(mapper.Map<ProdutoEditarResponse>(produto));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProdutoDTO>> EditarProduto(int id, ProdutoDTO produtoDTO) 
    {
        if (id != produtoDTO.ProdutoId)
        {
            return BadRequest();
        }

        var produto = mapper.Map<Produto>(produtoDTO);

        Produto produtoAtualizado = unitOfWork.ProdutosRepository.Editar(produto);

        await unitOfWork.Commit();

        var produtoDTOAtualizado = mapper.Map<ProdutoDTO>(produtoAtualizado);

        return Ok(produtoDTOAtualizado);         
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ProdutoDTO>> DeletarProduto(int id) 
    {
        Produto produto = await unitOfWork.ProdutosRepository.Buscar(x => x.ProdutoId == id);

        if (produto is null)
        {
            return NotFound("Produto não encontrado");
        }

        Produto produtoExcluido = unitOfWork.ProdutosRepository.Deletar(produto);

        await unitOfWork.Commit();

        var produtoDTOExcluido = mapper.Map<ProdutoDTO>(produtoExcluido);

        return Ok(produtoDTOExcluido);
    }
}
