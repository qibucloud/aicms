# AICMS - .NET 10 AOT CMS with Liquid Template Engine

一个使用 .NET 10 AOT 编译构建的高性能 CMS，具有 Liquid 模板引擎、多语言支持（中文、英文、日文）、分页和搜索功能。

A high-performance CMS built with .NET 10 AOT compilation, featuring Liquid template engine, multi-language support (Chinese, English, Japanese), pagination, and search functionality.

## Features | 功能特性

✅ **URL Router** - 多语言路由（中文/英文/日文）和自定义 URL 模式  
✅ **Liquid Template Engine** - 完全集成的 Liquid 模板支持  
✅ **Pagination** - 完整的分页功能  
✅ **Search Functionality** - 关键词过滤和消毒  
✅ **Independent Templates** - 每种语言完全独立的模板（无共享组件）  
✅ **Bootstrap Integration** - 开箱即用的响应式设计  
✅ **Breadcrumb Navigation** - 每种语言独立的面包屑导航  
✅ **AOT Compilation** - .NET 10 原生 AOT 编译，最大性能  

## Project Structure | 项目结构

```
aicms/
├── src/
│   ├── AICMS.Core/
│   │   ├── Routing/
│   │   │   ├── LanguageRouter.cs
│   │   │   ├── RouteContext.cs
│   │   │   ├── RouteBuilder.cs
│   │   │   └── LanguageDetector.cs
│   │   ├── Template/
│   │   │   ├── LiquidTemplateEngine.cs
│   │   │   ├── TemplateLoader.cs
│   │   │   └── TemplateCache.cs
│   │   ├── Pagination/
│   │   │   ├── PaginationHandler.cs
│   │   │   └── PageInfo.cs
│   │   ├── Search/
│   │   │   ├── SearchHandler.cs
│   │   │   └── SearchFilters.cs
│   │   └── Models/
│   │       ├── Product.cs
│   │       ├── Page.cs
│   │       └── NavigationModel.cs
│   ├── AICMS.Web/
│   │   ├── Program.cs
│   │   ├── Controllers/
│   │   │   ├── HomeController.cs
│   │   │   ├── ProductsController.cs
│   │   │   └── AboutController.cs
│   │   ├── Middleware/
│   │   │   └── MultiLanguageMiddleware.cs
│   │   └── appsettings.json
│   └── AICMS.Tests/
│       ├── RoutingTests.cs
│       ├── TemplateEngineTests.cs
│       └── PaginationTests.cs
├── templates/
│   ├── cn/
│   │   ├── layout.liquid
│   │   ├── index.liquid
│   │   ├── about.liquid
│   │   ├── products.liquid
│   │   ├── _header.liquid
│   │   ├── _footer.liquid
│   │   ├── _breadcrumb.liquid
│   │   └── _pagination.liquid
│   ├── en/
│   │   ├── layout.liquid
│   │   ├── index.liquid
│   │   ├── about.liquid
│   │   ├── products.liquid
│   │   ├── _header.liquid
│   │   ├── _footer.liquid
│   │   ├── _breadcrumb.liquid
│   │   └── _pagination.liquid
│   └── jp/
│       ├── layout.liquid
│       ├── index.liquid
│       ├── about.liquid
│       ├── products.liquid
│       ├── _header.liquid
│       ├── _footer.liquid
│       ├── _breadcrumb.liquid
│       └── _pagination.liquid
├── AICMS.sln
├── README.md
└── .gitignore
```

## URL Patterns | URL 模式

### Chinese | 中文
- `/` - 首页
- `/关于` - 关于
- `/产品` - 产品
- `/产品-第2页` - 产品第2页
- `/产品?keyword=led` - 产品搜索

### English | 英文
- `/en` - Home
- `/en/about` - About
- `/en/products` - Products
- `/en/products-page-2` - Products Page 2
- `/en/products?keyword=led` - Search Products

### Japanese | 日本語
- `/jp` - ホーム
- `/jp/概要` - 概要
- `/jp/商品` - 商品
- `/jp/商品-ページ2` - 商品ページ2
- `/jp/商品?keyword=led` - 商品検索

## Quick Start | 快速开始

### Prerequisites | 前置条件
- .NET 10 SDK 或更高版本
- Visual Studio 2022 或 VS Code

### Installation | 安装

```bash
git clone https://github.com/qibucloud/aicms.git
cd aicms
dotnet restore
```

### Running | 运行

```bash
cd src/AICMS.Web
dotnet run
```

应用将在 `http://localhost:5000` 可用

## Configuration | 配置

编辑 `src/AICMS.Web/appsettings.json`:

```json
{
  "Cms": {
    "TemplatesPath": "../../../templates",
    "EnableCache": true,
    "CacheDurationMinutes": 30,
    "DefaultLanguage": "cn",
    "SupportedLanguages": ["cn", "en", "jp"],
    "ItemsPerPage": 10
  }
}
```

## License

MIT
