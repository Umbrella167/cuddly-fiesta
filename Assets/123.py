import os
import pyperclip

def export_folder_structure_and_content(root_path, output_file, file_extensions):
    """
    遍历文件夹，导出目录结构和文件内容
    
    :param root_path: 根目录路径
    :param output_file: 输出文件名
    :param file_extensions: 要处理的文件扩展名列表
    """
    with open(output_file, 'w', encoding='utf-8') as f:
        # 写入目录结构
        f.write("目录结构:\n")
        for root, dirs, files in os.walk(root_path):
            level = root.replace(root_path, '').count(os.sep)
            indent = ' ' * 4 * level
            f.write(f"{indent}{os.path.basename(root)}/\n")
            subindent = ' ' * 4 * (level + 1)
            for file in files:
                if any(file.endswith(ext) for ext in file_extensions):
                    f.write(f"{subindent}{file}\n")
        
        # 写入文件内容
        f.write("\n文件内容:\n")
        for root, dirs, files in os.walk(root_path):
            for file in files:
                if any(file.endswith(ext) for ext in file_extensions):
                    file_path = os.path.join(root, file)
                    f.write(f"\n--- {file_path} ---\n")
                    try:
                        with open(file_path, 'r', encoding='utf-8') as content_file:
                            f.write(content_file.read())
                    except Exception as e:
                        f.write(f"无法读取文件: {e}\n")

    # 读取文件内容到剪贴板
    with open(output_file, 'r', encoding='utf-8') as f:
        content = f.read()
        pyperclip.copy(content)
    
    print(f"文件已导出到 {output_file} 并复制到剪贴板")

# 使用示例
if __name__ == "__main__":
    # 设置根目录路径
    root_path = r"D:\MyGme\cuddly-fiesta\Assets"
    
    # 设置输出文件名
    output_file = "project_structure_and_content.txt"
    
    # 设置要处理的文件扩展名
    file_extensions = ['.cs', '.py', '.txt', '.json']
    
    # 调用函数
    export_folder_structure_and_content(root_path, output_file, file_extensions)