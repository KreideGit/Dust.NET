#version 330 core
layout (location = 0) in vec3 position; 
layout (location = 1) in vec2 tex;
  
out vec2 o_tex;
 
 uniform mat4 mvp;
  
void main()
{
    o_tex = tex;
    gl_Position = mvp * vec4(position, 1.0); 
}