Phases Umbraco Examine Search Extension
An advanced search extension for Umbraco CMS that enhances Examine's search capabilities with features like fuzzy matching, spell checking, language detection, and term highlighting.

Features
1. Advanced Search Term Processing
   
    Fuzzy Matching: Find similar terms with configurable fuzziness
   
    Term Boosting: Increase relevance of specific search terms
   
    Proximity Search: Match terms within specified distances
   
    Wildcard Support:
      Single character wildcards
      Multiple character wildcards
   
    Stop Word Removal: Automatic filtering of common words
   
    Term Escaping: Literal term matching
   
3. Search Results Management
   
    Pagination: Built-in support for paged results
   
    Type Conversion: Convert search results to strongly-typed models
   
    Culture-Specific Searches: Search across different languages
   
    Template Filtering: Search by template IDs
   
    Publication Status: Filter by published/unpublished content
   
    Navigation Visibility: Filter by navigation visibility
   
5. Text Processing
   
    HTML Content Extraction: Clean text extraction from HTML content
   
    Term Highlighting: Highlight matched terms in results
   
    Search Fragment Generation: Create relevant result snippets
   
    Language Detection: Automatic language identification
   
7. Spell Checking
   
    Suggestions: Get spelling suggestions for search terms
   
    Configurable Accuracy: Adjust suggestion accuracy
   
    Custom Dictionary Support: Use field-specific dictionaries

Installation(Enable pre-release)

    dotnet add package Phases.Umb.Examine.Search.Extension

Configuration

Add to appsettings.json:
    {
      "SpellCheckOptions": {
        "IndexName": "ExternalIndex",
        "IndexedFields": ["nodeName"],
        "AutoRebuildIndex": false,
        "AutoRebuildDelay": 5,
        "AutoRebuildRepeat": 30,
        "EnableLogging": false
      }
    }
    
Usage Examples

Basic Search

    // Create search query
    var searcher = examineManager.GetSearcher("ExternalIndex");
    var query = searcher.CreateQueryWithCategory();
    
    // Execute search
    var results = query.PerformSearch<MyContentType>();
    
Advanced Term Processing

     // Process search terms
    string[] terms = "search query".ToSafeArray();
    
    // Apply various search enhancements
    var boostedTerms = terms.Boost(1.5f);
    var fuzzyTerms = terms.Fuzzy(0.5f);
    var proximityTerms = terms.Proximity(3);
    var wildcardTerms = terms.MultipleCharacterWildcard();

Pagination

    // Get paged results
    var results = query.PerformSearchByPage<MyContentType>(
        page: 1,
        perPage: 10,
        out int totalPages,
        out int totalResults
    );
    
Text Highlighting

     var helper = new StringFormatHelper();

    // Extract clean text
    string plainText = helper.ExtractTextFromHtml(htmlContent);
    
    // Highlight search terms
    string highlighted = helper.HighlightString(content, searchTerms);
    
 Spell Checking
 
      var suggestionService = new SuggestionService(options, environment);
      
      // Get spelling suggestions
      string suggestion = suggestionService.GetSuggestion(
          searchTerm: "serch",
          numberOfSuggestions: 10,
          suggestionAccuracy: 0.75f
      );
      
 Language Detection  
 
      var customService = new CustomService();
      var languageInfo = customService.DetectLanguage(content);
      
Requirements

        Umbraco 10.7.0+
        
        .NET 6.0+
        
        Examine 3.1.0+
        
        Lucene.NET 4.8.0-beta00016
  
Dependencies

        HtmlAgilityPack
        
        NTextCat
        
        Lucene.Net.Analysis.Common
        
        Lucene.Net.QueryParser
        
        Lucene.Net.Highlighter

Support
  Email: jerinjosec23@gmail.com
