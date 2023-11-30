export function normalizeSpecialCharactersOnString(str) {
    // Remove acentos
    const normalizedString = str.normalize("NFD").replace(/[\u0300-\u036f]/g, "");

    // Substitui caracteres especiais
    const replacedString = normalizedString
        .replace(/[����]/g, "a")
        .replace(/[�]/g, "c")
        .replace(/[��]/g, "e");

    return replacedString;
}