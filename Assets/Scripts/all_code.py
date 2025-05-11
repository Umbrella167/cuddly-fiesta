import os
import json
import chardet

def detect_encoding(file_path):
    """
    检测文件编码
    """
    with open(file_path, 'rb') as file:
        raw_data = file.read()
        result = chardet.detect(raw_data)
        return result['encoding']

def read_file_with_encoding(file_path):
    """
    尝试读取文件，处理不同编码
    """
    # 支持的编码列表
    encodings = ['utf-8', 'gb2312', 'gbk', 'big5']
    
    # 首先使用chardet检测编码
    detected_encoding = detect_encoding(file_path)
    
    # 尝试的编码列表，优先使用检测到的编码
    try_encodings = [detected_encoding] + [enc for enc in encodings if enc != detected_encoding]
    
    for encoding in try_encodings:
        try:
            with open(file_path, 'r', encoding=encoding) as f:
                return f.read()
        except (UnicodeDecodeError, LookupError):
            continue
    
    # 如果所有编码都失败，尝试使用错误处理
    for encoding in try_encodings:
        try:
            with open(file_path, 'r', encoding=encoding, errors='ignore') as f:
                return f.read()
        except Exception:
            continue
    
    # 如果仍然失败
    raise ValueError(f"Unable to read file with any encoding: {file_path}")

def get_py_files_info(root_dir, out_dir_name):
    """遍历目录并收集所有Python文件信息"""
    py_files = []
    
    for dirpath, _, filenames in os.walk(root_dir):
        all_dir = dirpath.split(os.path.sep)
        is_continue = any(name in all_dir for name in out_dir_name)
        
        if is_continue:
            continue
        
        for filename in filenames:
            if filename.endswith('.cs'):
                file_path = os.path.join(dirpath, filename)
                file_info = {
                    "file_path": os.path.abspath(file_path),
                    "relative_path": os.path.relpath(file_path, root_dir),
                    "file_name": filename,
                    "content": ""
                }

                try:
                    file_info["content"] = read_file_with_encoding(file_path)
                    py_files.append(file_info)
                except Exception as e:
                    print(f"读取文件 {file_path} 失败: {str(e)}")

    return py_files

def save_to_json(data, output_file):
    """将数据保存为JSON文件"""
    with open(output_file, 'w', encoding='utf-8') as f:
        json.dump(data, f, ensure_ascii=False, indent=2)

if __name__ == '__main__':
    # 配置参数
    project_root = r"D:\MyGme\cuddly-fiesta\Assets\Scripts"
    output_filename = 'python_files_output.json'
    out_dir = ['Proto']
    
    # 收集文件信息
    files_data = get_py_files_info(project_root, out_dir)

    # 保存到JSON文件
    save_to_json(files_data, output_filename)
    print(f"成功处理 {len(files_data)} 个Python文件，结果已保存到 {output_filename}")