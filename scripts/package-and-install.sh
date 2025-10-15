#!/bin/bash
# 自动编译、打包并安装 VSCode 插件

# 输出目录
OUTPUT_DIR="./release"
mkdir -p $OUTPUT_DIR

echo "=== Step 1: 编译 TypeScript ==="
npm run compile
if [ $? -ne 0 ]; then
  echo "编译失败，停止打包"
  exit 1
fi

echo "=== Step 2: 打包 VSIX ==="
npx vsce package
if [ $? -ne 0 ]; then
  echo "打包失败"
  exit 1
fi

# 获取生成的 VSIX 文件
VSIX_FILE=$(ls *.vsix | head -n 1)
if [ -z "$VSIX_FILE" ]; then
  echo "没有找到生成的 VSIX 文件"
  exit 1
fi

# 移动到 release 目录
mv "$VSIX_FILE" "$OUTPUT_DIR/"
VSIX_PATH="$OUTPUT_DIR/$VSIX_FILE"
echo "打包完成，VSIX 文件已保存到 $VSIX_PATH"

echo "=== Step 3: 安装到 VSCode ==="
code --install-extension "$VSIX_PATH" --force
if [ $? -eq 0 ]; then
  echo "插件安装成功！"
else
  echo "插件安装失败，请确保 'code' 命令可用"
  exit 1
fi
echo "请重启 VSCode 以应用插件"