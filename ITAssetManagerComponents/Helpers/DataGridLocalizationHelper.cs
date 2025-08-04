using Blazorise.Localization;

namespace ITAssetManagerComponents.Helpers
{
    public static class DataGridLocalizationHelper
    {
        public static TextLocalizationResource GetKoreanDataGridResource()
        {
            return new TextLocalizationResource
            {
                Culture = "ko-KR",
                Translations = new Dictionary<string, string>()
                {
                    { "First", "처음" },
                    { "Prev", "이전" },
                    { "Next", "다음" },
                    { "Last", "마지막" },
                    { "items per page", "페이지당 항목 수" },
                    { "of", "중" },
                    { "items", "항목" },
                    { "Page", "페이지" },
                    { "Go to page", "페이지로 이동" },
                    { "Show entries", "항목 표시" },
                    { "Showing", "표시 중" },
                    { "to", "~" },
                    { "of {0} entries", "총 {0}개 항목" },
                    { "(filtered from {0} total entries)", "(총 {0}개 항목에서 필터링)" },
                    { "No data available", "데이터가 없습니다" },
                    { "Search:", "검색:" },
                    { "Show", "표시" },
                    { "entries", "항목" },
                    { "Previous", "이전" },
                    { "Loading...", "로딩 중..." },
                    { "Processing...", "처리 중..." },
                    { "Search", "검색" },
                    { "Length menu", "페이지당 _MENU_ 항목 표시" },
                    { "Info", "_START_에서 _END_까지 표시 (총 _TOTAL_ 항목)" },
                    { "Info empty", "0에서 0까지 표시 (총 0 항목)" },
                    { "Info filtered", "(총 _MAX_ 항목에서 필터링됨)" },
                    { "Info postfix", "" },
                    { "Thousands", "," },
                    { "Decimal", "." },
                    { "Empty table", "테이블에 데이터가 없습니다" },
                    { "Paginate first", "처음" },
                    { "Paginate last", "마지막" },
                    { "Paginate next", "다음" },
                    { "Paginate previous", "이전" },
                    // 추가 번역 키들 (DataGrid pagination info 관련)
                    { "{0} - {1} of {2} items", "{0} - {1} / {2} 항목" },
                    { "{0} of {1} items", "{0} / {1} 항목" },
                    { "Showing {0} to {1} of {2} entries", "{0}에서 {1}까지 표시 (총 {2} 항목)" },
                    { "Showing {0} to {1} of {2} entries (filtered from {3} total entries)", "{0}에서 {1}까지 표시 (총 {3} 항목에서 {2}개 필터링)" },
                    { "0 to 0 of 0 entries", "0에서 0까지 표시 (총 0 항목)" },
                    { "Displaying items {0} - {1} of {2}", "{0} - {1} / {2} 항목 표시" },
                    { "Displaying items", "항목 표시" },
                    { "Page {0} of {1}", "{0} / {1} 페이지" }
                }
            };
        }
    }
}