namespace Lextatico.Sly.Result
{
    public enum ErrorCodes
    {
        NotAnError = -1,
        
        #region Lexer
        
        
        LexerUnknownError,
        
        LexerDuplicateStringCharDelimiters,
        
        // LEXER_TOO_MANY_COMMNENT,
        
        // LEXER_TOO_MANY_MULTILINE_COMMNENT,
        
        // LEXER_TOO_MANY_SINGLELINE_COMMNENT,
        
        // LEXER_CANNOT_MIX_COMMENT_AND_SINGLE_OR_MULTI,
        
        // LEXER_SAME_VALUE_USED_MANY_TIME,
        
        LexerStringDelimiterMustBe1Char,
        
        LexerStringDelimiterCannotBeLetterOrDigit,
        
        LexerCharDelimiterMustBe1Char,
        
        LexerCharDelimiterCannotBeLetter,
        
        LexerSugarTokenCannotStartWithLetter,
        
        #endregion

        #region Parser

        // PARSER_UNKNOWN_ERROR,
        
        // PARSER_MISSING_OPERAND,
        
        ParserReferenceNotFound,
        
        // PARSER_MIXED_CHOICES,
        
        // PARSER_NON_TERMINAL_CHOICE_CANNOT_BE_DISCARDED,
        
        // PARSER_INCORRECT_VISITOR_RETURN_TYPE,
        
        // PARSER_INCORRECT_VISITOR_PARAMETER_TYPE,
        
        // PARSER_INCORRECT_VISITOR_PARAMETER_NUMBER,
        
        ParserLeftRecursive,

        #endregion
    }
}