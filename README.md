# Fold to Definition (VS2022 Style)

VSCode C# 插件，带来 **VS2022 风格的折叠与格式化体验**。  
一键折叠代码到定义（保留 summary 注释），支持展开所有、格式化文档及 VS2022 风格快捷键。

---

## 功能

- **VS2022 风格折叠到定义**：保留 XML summary 注释  
- **展开所有折叠块**  
- **格式化文档**  
- 快捷键模拟 VS2022：  
  - 折叠到定义：`Cmd+M, Cmd+O`  
  - 展开所有折叠：`Cmd+M, Cmd+L`  
  - 格式化文档：`Cmd+K, Cmd+D`  
- **右键菜单支持**：编辑器、资源管理器、导航栏  
- **仅在 C# 文件中生效**

---

## 安装

1. 下载最新 `.vsix` 文件  
2. 在 VSCode 中执行 **Extensions: Install from VSIX**  

---

## 开发

```bash
# 安装依赖
npm install

# 开发调试模式
npm run watch
