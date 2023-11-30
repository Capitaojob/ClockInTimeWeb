export function normalizeSpecialCharactersOnString(str) {
    // Remove acentos
    const normalizedString = str.normalize("NFD").replace(/[\u0300-\u036f]/g, "");

    // Substitui caracteres especiais
    const replacedString = normalizedString
        .replace(/[гваб]/g, "a")
        .replace(/[з]/g, "c")
        .replace(/[йк]/g, "e");

    return replacedString;
}