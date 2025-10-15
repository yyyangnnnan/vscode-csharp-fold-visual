import * as vscode from "vscode";

export function activate(context: vscode.ExtensionContext) {

  // -------------------------------------------------------
  //  VS2022 风格折叠到定义
  // -------------------------------------------------------
  const foldDisposable = vscode.commands.registerCommand("foldToDefinition.fold", async () => {
    const editor = vscode.window.activeTextEditor;
    if (!editor || editor.document.languageId !== "csharp") {
      vscode.window.showWarningMessage("此命令仅支持 C# 文件。");
      return;
    }

    const originalSelections = editor.selections;

    await vscode.window.withProgress({
      location: vscode.ProgressLocation.Window,
      title: "正在折叠到定义...",
      cancellable: false
    }, async () => {

      const allFoldingRanges = await vscode.commands.executeCommand<vscode.FoldingRange[]>(
        "vscode.executeFoldingRangeProvider",
        editor.document.uri
      );

      if (!allFoldingRanges || allFoldingRanges.length === 0) return;

      const linesToFold: number[] = [];
      const summaryCommentRegex = /^\s*\/\/\/\s*<summary>/;

      for (const range of allFoldingRanges) {
        if (range.kind === vscode.FoldingRangeKind.Comment) {
          const startLineText = editor.document.lineAt(range.start).text;
          const nextLineText = editor.document.lineAt(Math.min(range.start + 1, editor.document.lineCount - 1)).text;
          if (!summaryCommentRegex.test(startLineText) && !summaryCommentRegex.test(nextLineText)) {
            linesToFold.push(range.start);
          }
        } else {
          linesToFold.push(range.start);
        }
      }

      if (linesToFold.length > 0) {
        await vscode.commands.executeCommand("editor.unfoldAll");
        editor.selections = linesToFold.map(line => new vscode.Selection(line, 0, line, 0));
        await vscode.commands.executeCommand("editor.fold");
      }
    });

    editor.selections = originalSelections;
    if (originalSelections.length > 0) {
      editor.revealRange(originalSelections[0], vscode.TextEditorRevealType.Default);
    }
  });

  // -------------------------------------------------------
  //  展开所有
  // -------------------------------------------------------
  const unfoldDisposable = vscode.commands.registerCommand("foldToDefinition.unfold", async () => {
    const editor = vscode.window.activeTextEditor;
    if (!editor || editor.document.languageId !== "csharp") {
      vscode.window.showWarningMessage("此命令仅支持 C# 文件。");
      return;
    }
    await vscode.commands.executeCommand("editor.unfoldAll");
  });

  context.subscriptions.push(foldDisposable, unfoldDisposable);
}

export function deactivate() {}
