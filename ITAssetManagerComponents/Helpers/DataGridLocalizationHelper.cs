using Blazorise.Localization;

namespace ITAssetManagerComponents.Helpers
{
    public static class DataGridLocalizationHelper
    {
        public const string KoreanCulture = "ko-KR";
        public static TextLocalizationResource GetKoreanDataGridResource()
        {
            return new TextLocalizationResource
            {
                Culture = KoreanCulture,
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
                    { "Page {0} of {1}", "{0} / {1} 페이지" },
                    { "Filter", "필터" },
                    { "Clear filter", "필터 지우기" },
                    
                    // DataGrid 필터 옵션들의 한글 번역
                    { "Contains", "포함" },
                    { "Starts With", "시작" },
                    { "Ends With", "끝남" },
                    { "Equals", "같음" },
                    { "Not Equals", "같지 않음" },
                    { "Less Than", "작음" },
                    { "Less Than Or Equal", "작거나 같음" },
                    { "Greater Than", "큼" },
                    { "Greater Than Or Equal", "크거나 같음" },
                    { "Is Null", "비어있음" },
                    { "Is Not Null", "비어있지 않음" },
                    { "Is Empty", "공백" },
                    { "Is Not Empty", "공백 아님" },
                    
                    // 필터 메뉴 관련 번역
                    { "Filter Menu", "필터 메뉴" },
                    { "Apply Filter", "필터 적용" },
                    { "Clear Filter", "필터 지우기" },
                    { "Filter Value", "필터 값" },
                    { "Filter Method", "필터 방법" },
                    { "Select Filter Method", "필터 방법 선택" },
                    { "Enter Filter Value", "필터 값 입력" },
                    
                    // 추가적인 DataGrid 관련 번역
                    { "All", "전체" },
                    { "Select All", "전체 선택" },
                    { "None", "없음" },
                    { "OK", "확인" },
                    { "Cancel", "취소" },
                    { "Apply", "적용" },
                    { "Reset", "초기화" },
                }
            };
        }
    }
}