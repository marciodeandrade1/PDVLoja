# PDVLoja
# PDV Loja Completo

## Descrição

Este repositório contém o projeto completo de um **Sistema de Ponto de Venda (PDV)** desenvolvido em C# com Windows Forms, utilizando SQL Server local como banco de dados. O sistema é projetado para a frente de caixa de uma pequena loja, com integração nativa para controle de estoque, processamento de pagamentos via PIX, Cartão de Crédito/Débito, Dinheiro, e integração com conta bancária (via APIs como Mercado Pago ou PagSeguro). 

O desenvolvimento seguiu uma abordagem estruturada:
- **Diagramas Iniciais**: Fluxogramas de processos (vendas, estoque, relatórios), diagramas de atividades UML e diagramas de classes para modelar o sistema.
- **Arquitetura**: Adotada uma arquitetura em camadas (UI, Business Logic, Data Access, Integrações) com padrões MVVM para escalabilidade e manutenção. Suporte a multi-caixa via conexão compartilhada ao SQL Server em rede local.
- **Código Padronizado**: Uso de Entity Framework Core para ORM, injeção de dependência (DI), transações atômicas, validações com ErrorProvider, e mocks iniciais para integrações de pagamento.

O foco é em manutenção fácil (código modular, testes unitários sugeridos) e escalabilidade (fácil migração para WPF/Web, adição de features como SignalR para real-time).

## Funcionalidades Principais

- **Vendas**: Adição de produtos via código de barras, cálculo de total, descontos, e finalização com atualização automática de estoque.
- **Pagamentos**: Suporte a PIX (QR Code), Cartão Crédito/Débito (tokenização), Dinheiro. Integração com Mercado Pago/PagSeguro para processamento real e depósito em conta bancária.
- **Controle de Estoque**: Adição/edição/removação de produtos, alertas para estoque mínimo, projeções baseadas em vendas.
- **Relatórios e Dashboard**: Relatórios em PDF/Excel (itens mais vendidos, estoque baixo), dashboard com gráficos (barras, linhas) usando Chart controls.
- **Autenticação**: Login com hash de senhas (BCrypt).
- **Multi-Caixa**: Conexão compartilhada ao SQL Server via LAN; atualizações em tempo real.
- **Validações**: Campos obrigatórios, formatos (ex: regex para cartões), regras de negócio (ex: estoque > mínimo).
- **Migrações e Seeding**: EF Core para schema e dados iniciais (usuários/produtos default).

## Requisitos

- **Ambiente de Desenvolvimento**:
  - Visual Studio 2022+ com workload .NET Desktop.
  - .NET 8.0 SDK.
  - SQL Server Express/LocalDB (para dev) ou SQL Server full (para multi-caixa em produção).

- **NuGet Packages**:
  - Microsoft.EntityFrameworkCore.SqlServer
  - Microsoft.EntityFrameworkCore.Tools
  - BCrypt.Net-Next
  - EPPlus (Excel)
  - iTextSharp (PDF)
  - System.Windows.Forms.DataVisualization (Gráficos)
  - MercadoPago.SDK (para integração Mercado Pago)
  - PagSeguro.SDK (opcional para PagSeguro)

- **Hardware/Software para Execução**:
  - Windows 10+.
  - Impressora para recibos (suporte ESC/POS via helper).
  - Rede LAN para multi-caixa.

## Instalação

1. **Clone o Repositório**:
   ```
   git clone https://github.com/seuusuario/PDV_LojaCompleto.git
   cd PDV_LojaCompleto
   ```

2. **Restaure Pacotes**:
   ```
   dotnet restore
   ```

3. **Configure Connection String**: Edite `Helpers/ConnectionHelper.cs` para apontar ao seu SQL Server (ex: local ou IP para multi-caixa).
   ```
   return "Server=SEU_SERVIDOR;Database=PDV_LojaDB;Trusted_Connection=True;";
   ```

4. **Aplique Migrações e Seeding**:
   ```
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
   - Isso cria o DB com tabelas e dados iniciais (usuário: admin / senha: admin123).

5. **Configure Integrações de Pagamento**:
   - Para Mercado Pago: Adicione Access Token em `Helpers/MercadoPagoConfig.cs`.
   - Para PagSeguro: Similar, configure email/token.
   - Teste em sandbox antes de produção.

6. **Compile e Execute**:
   - Abra no Visual Studio e rode (F5).
   - Para multi-caixa: Instale o executável em múltiplas máquinas, apontando ao mesmo DB.

## Uso

- **Login**: Use credenciais default ou crie novos via módulo de usuários.
- **Venda**: No menu principal, selecione "Nova Venda", adicione itens, escolha pagamento e finalize.
- **Estoque**: Módulo para gerenciar produtos e alertas.
- **Relatórios/Dashboard**: Filtre por data, exporte ou visualize gráficos.
- **Multi-Caixa**: Certifique-se de que todas as máquinas acessam o mesmo SQL Server; transações evitam inconsistências.

Exemplo de Fluxograma Simples (em Markdown):
```
Início → Login → Menu Principal → Nova Venda → Adicionar Itens → Pagamento → Atualizar Estoque → Emitir Recibo → Fim
```

Para diagramas detalhados, consulte o histórico de desenvolvimento no repositório (arquivos de texto com representações UML).

## Estrutura de Pastas

- **Forms/**: Interfaces WinForms (frmVenda.cs, frmDashboard.cs, etc.).
- **Models/**: Entidades (Produto.cs, Venda.cs).
- **Data/**: PdvContext.cs e migrações.
- **Services/**: Lógica de negócios (VendaService.cs, RelatorioService.cs).
- **ViewModels/**: MVVM para dashboard.
- **Helpers/**: Utilitários (ConnectionHelper.cs, CryptoHelper.cs).

## Contribuição

1. Fork o repositório.
2. Crie uma branch: `git checkout -b feature/nova-feature`.
3. Commit: `git commit -m "Adiciona nova feature"`.
4. Push: `git push origin feature/nova-feature`.
5. Abra um Pull Request.

Siga padrões: CamelCase para vars, PascalCase para classes. Adicione testes unitários para services.

## Licença

MIT License. Veja [LICENSE](LICENSE) para detalhes.

## Contato

Para questões: marcio@requestdev.net ou abra uma issue no GitHub.

Este projeto é open-source e visa ajudar pequenas lojas a gerenciarem vendas de forma eficiente!
