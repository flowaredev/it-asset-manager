using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITAssetManagerLibrary.Helpers
{
    public class ValidationMessageLocalizingHelper
    {
        private static readonly Dictionary<string, string> _applicationLocalizerDictionary = new()
        {
            { "The {0} field is required.", "{0} 필드는 필수입니다." },
            { "The {0} field is not a valid e-mail address.", "{0} 필드는 유효한 이메일 주소가 아닙니다." },
            { "The field {0} must be a string or array type with a minimum length of '{1}'.", "{0} 필드는 최소 {1}자 이상이어야 합니다." },
            { "The field {0} must be a string or array type with a maximum length of '{1}'.", "{0} 필드는 최대 {1}자 이하여야 합니다." },
            { "The field {0} must be between {1} and {2}.", "{0} 필드는 {1}에서 {2} 사이여야 합니다." },
            { "Passwords must be at least {0} characters.", "비밀번호는 최소 {0}자 이상이어야 합니다." },
            { "Passwords must have at least one non alphanumeric character.", "비밀번호는 최소한 하나의 특수문자를 포함해야 합니다." },
            { "Passwords must have at least one digit ('0'-'9').", "비밀번호는 최소한 하나의 숫자('0'-'9')를 포함해야 합니다." },
            { "Passwords must have at least one uppercase ('A'-'Z').", "비밀번호는 최소한 하나의 대문자('A'-'Z')를 포함해야 합니다." },
            { "Passwords must have at least one lowercase ('a'-'z').", "비밀번호는 최소한 하나의 소문자('a'-'z')를 포함해야 합니다." },
            
            // 로그인 관련 메시지
            { "Error: Invalid login attempt.", "오류: 잘못된 로그인 시도입니다." },
            { "Invalid login attempt.", "잘못된 로그인 시도입니다." },
            { "User account locked out.", "사용자 계정이 잠겨있습니다." },
            { "User logged in.", "사용자가 로그인했습니다." },
            
            // 회원가입 관련 메시지
            { "User created a new account with password.", "사용자가 비밀번호로 새 계정을 생성했습니다." },
            { "The {0} must be at least {2} and at max {1} characters long.", "{0}은(는) 최소 {2}자 이상 최대 {1}자 이하여야 합니다." },
            { "The password and confirmation password do not match.", "비밀번호와 비밀번호 확인이 일치하지 않습니다." },
            { "The new password and confirmation password do not match.", "새 비밀번호와 비밀번호 확인이 일치하지 않습니다." },
            
            // 이메일 관련 메시지
            { "Verification email sent. Please check your email.", "인증 이메일이 발송되었습니다. 이메일을 확인해주세요." },
            { "Your email is unchanged.", "이메일이 변경되지 않았습니다." },
            { "Confirmation link to change email sent. Please check your email.", "이메일 변경 확인 링크가 발송되었습니다. 이메일을 확인해주세요." },
            { "Your password has been changed", "비밀번호가 변경되었습니다." },
            
            // Identity 에러 관련 메시지
            { "User name '{0}' is already taken.", "사용자명 '{0}'은(는) 이미 사용 중입니다." },
            { "Email '{0}' is already taken.", "이메일 '{0}'은(는) 이미 사용 중입니다." },
            { "User name cannot be null or empty.", "사용자명은 비어있을 수 없습니다." },
            { "A user with this login already exists.", "이 로그인 정보를 가진 사용자가 이미 존재합니다." },
            { "Invalid user name '{0}'.", "잘못된 사용자명 '{0}'입니다." },
            { "Invalid email '{0}'.", "잘못된 이메일 '{0}'입니다." },
            { "Duplicate user name.", "중복된 사용자명입니다." },
            { "Duplicate email.", "중복된 이메일입니다." },
            { "Role name '{0}' is already taken.", "역할명 '{0}'은(는) 이미 사용 중입니다." },
            { "Role name '{0}' is invalid.", "역할명 '{0}'은(는) 잘못되었습니다." },
            
            // 일반적인 필드명 번역
            { "Email", "이메일" },
            { "Password", "비밀번호" },
            { "ConfirmPassword", "비밀번호 확인" },
            { "CurrentPassword", "현재 비밀번호" },
            { "NewPassword", "새 비밀번호" },
            { "RememberMe", "로그인 상태 유지" },
            { "UserName", "사용자명" },
            { "PhoneNumber", "전화번호" },
            { "Confirm password", "비밀번호 확인" },
            { "Current password", "현재 비밀번호" },
            { "New password", "새 비밀번호" },
            { "Confirm new password", "새 비밀번호 확인" },
        };

        public static string GetValidationMessage(string message, IEnumerable<string> arguments)
        {
            if (_applicationLocalizerDictionary.ContainsKey(message))
            {
                return string.Format(_applicationLocalizerDictionary[message], [.. arguments]);
            }
            return string.Format(message, [.. arguments]);
        }

        public static string GetLocalizedMessage(string message)
        {
            return _applicationLocalizerDictionary.ContainsKey(message) ? _applicationLocalizerDictionary[message] : message;
        }

        public static string GetLocalizedIdentityErrors(IEnumerable<string> errors)
        {
            var translatedErrors = errors.Select(error => GetLocalizedMessage(error));
            return string.Join(", ", translatedErrors);
        }
    }
}
