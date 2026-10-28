param(
    [string]$InputDir = 'C:\Users\pc\Downloads\Project-int\_codex_reports\fusion_audit_input\Fusion_Vien_Vang_Game_FIXED\output',
    [string]$OutputPath = 'C:\Users\pc\Downloads\Project-int\Project int\_codex_reports\fusion_part_audit\FUSION_CLIENT_FORMULA_SIMULATION.png'
)

Add-Type -AssemblyName System.Drawing

$head = @(
    @{ id = 26155; dx = -2; dy = -22 },
    @{ id = 26157; dx = -1; dy = -24 },
    @{ id = 2955;  dx = 0;  dy = 0 }
)
$leg = @(
    @{ id = 26274; dx = 4;  dy = 0 }, @{ id = 26275; dx = -3; dy = -8 },
    @{ id = 26276; dx = -5; dy = -8 }, @{ id = 26277; dx = -5; dy = -34 },
    @{ id = 26278; dx = -6; dy = -34 }, @{ id = 26279; dx = -2; dy = -37 },
    @{ id = 26280; dx = -5; dy = -33 }, @{ id = 26281; dx = -3; dy = -4 },
    @{ id = 26282; dx = -2; dy = -6 }, @{ id = 26283; dx = -6; dy = -8 },
    @{ id = 26284; dx = 3;  dy = -7 }, @{ id = 26285; dx = 1;  dy = -9 },
    @{ id = 26286; dx = 0;  dy = -7 }, @{ id = 2955;  dx = 0;  dy = 0 }
)
$body = @(
    @{ id = 26262; dx = -5;  dy = -9 }, @{ id = 26263; dx = -4;  dy = -19 },
    @{ id = 26264; dx = -7;  dy = -26 }, @{ id = 2955;  dx = 0;   dy = 0 },
    @{ id = 2955;  dx = 0;   dy = 0 }, @{ id = 2955;  dx = 0;   dy = 0 },
    @{ id = 2955;  dx = 0;   dy = 0 }, @{ id = 26265; dx = -4;  dy = -24 },
    @{ id = 26266; dx = 1;   dy = -20 }, @{ id = 26267; dx = -6;  dy = -18 },
    @{ id = 26268; dx = -12; dy = -43 }, @{ id = 26269; dx = -4;  dy = -24 },
    @{ id = 26270; dx = 0;   dy = -25 }, @{ id = 26271; dx = -2;  dy = -22 },
    @{ id = 26272; dx = -6;  dy = -21 }, @{ id = 26273; dx = -7;  dy = -22 },
    @{ id = 2955;  dx = 0;   dy = 0 }
)

# [head index,x,y], [leg index,x,y], [body index,x,y] copied from Char.CharInfo.
$frames = @{
    0  = @(@(0,-13,34), @(1,-8,10),  @(1,-9,16))
    1  = @(@(0,-13,35), @(1,-8,10),  @(1,-9,17))
    2  = @(@(1,-10,33), @(2,-10,11), @(2,-8,16))
    3  = @(@(1,-10,32), @(3,-12,10), @(3,-11,15))
    4  = @(@(1,-10,34), @(4,-8,11),  @(4,-7,17))
    5  = @(@(1,-10,34), @(5,-12,11), @(5,-9,17))
    6  = @(@(1,-10,33), @(6,-10,10), @(6,-8,16))
    7  = @(@(0,-9,36),  @(7,-5,17),  @(7,-11,25))
    8  = @(@(0,-7,35),  @(0,-18,22), @(7,-10,25))
    13 = @(@(1,-9,34),  @(9,-12,9),  @(10,-7,19))
    14 = @(@(1,-13,34), @(9,-12,9),  @(11,-10,19))
    26 = @(@(1,-9,34),  @(8,-5,13),  @(10,-7,19))
    27 = @(@(1,-13,34), @(8,-5,13),  @(11,-10,19))
}
$poses = @(
    @{ label = 'Dung cf0'; cf = 0 }, @{ label = 'Dung cf1'; cf = 1 },
    @{ label = 'Chay cf2'; cf = 2 }, @{ label = 'Chay cf3'; cf = 3 },
    @{ label = 'Chay cf4'; cf = 4 }, @{ label = 'Chay cf5'; cf = 5 },
    @{ label = 'Chay cf6'; cf = 6 }, @{ label = 'Bay cf7'; cf = 7 },
    @{ label = 'Bay cf8'; cf = 8 }, @{ label = 'Danh dat cf13'; cf = 13 },
    @{ label = 'Danh dat cf14'; cf = 14 }, @{ label = 'Danh bay cf26'; cf = 26 },
    @{ label = 'Danh bay cf27'; cf = 27 }
)

function Get-ImagePath([int]$id) {
    if ($id -eq 2955) { return $null }
    $path = Join-Path $InputDir "$id.png"
    if (Test-Path -LiteralPath $path) { return $path }
    return $null
}

function Draw-Part($graphics, $partEntry, $frameEntry, [bool]$left, [int]$originX, [int]$originY) {
    $path = Get-ImagePath $partEntry.id
    if ($null -eq $path) { return $false }
    $source = [System.Drawing.Bitmap]::FromFile($path)
    try {
        # Image.getWidth()/getHeight() floor-divide by zoom; drawRegion multiplies back.
        $drawWidth = [Math]::Floor($source.Width / 4) * 4
        $drawHeight = [Math]::Floor($source.Height / 4) * 4
        $sumX = [int]$frameEntry[1] + [int]$partEntry.dx
        $top = $originY + ((-[int]$frameEntry[2] + [int]$partEntry.dy) * 4)
        if (-not $left) {
            $dest = [System.Drawing.Rectangle]::new($originX + $sumX * 4, $top, $drawWidth, $drawHeight)
            $graphics.DrawImage($source, $dest, 0, 0, $drawWidth, $drawHeight, [System.Drawing.GraphicsUnit]::Pixel)
        } else {
            $copy = $source.Clone([System.Drawing.Rectangle]::new(0, 0, $drawWidth, $drawHeight), [System.Drawing.Imaging.PixelFormat]::Format32bppArgb)
            try {
                $copy.RotateFlip([System.Drawing.RotateFlipType]::RotateNoneFlipX)
                $targetX = $originX - $sumX * 4
                $dest = [System.Drawing.Rectangle]::new($targetX - $drawWidth, $top, $drawWidth, $drawHeight)
                $graphics.DrawImageUnscaled($copy, $dest.X, $dest.Y)
            } finally { $copy.Dispose() }
        }
    } finally { $source.Dispose() }
    return $true
}

$cellW = 340
$cellH = 390
$cols = 4
$rows = [Math]::Ceiling(($poses.Count * 2) / $cols)
$atlas = [System.Drawing.Bitmap]::new($cellW * $cols, $cellH * $rows, [System.Drawing.Imaging.PixelFormat]::Format32bppArgb)
$g = [System.Drawing.Graphics]::FromImage($atlas)
try {
    $g.Clear([System.Drawing.Color]::FromArgb(255, 38, 42, 48))
    $g.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::NearestNeighbor
    $g.PixelOffsetMode = [System.Drawing.Drawing2D.PixelOffsetMode]::Half
    $font = [System.Drawing.Font]::new('Arial', 12, [System.Drawing.FontStyle]::Bold)
    $smallFont = [System.Drawing.Font]::new('Arial', 9)
    $pen = [System.Drawing.Pen]::new([System.Drawing.Color]::FromArgb(120, 180, 180, 180), 1)
    try {
        for ($poseIndex = 0; $poseIndex -lt $poses.Count; $poseIndex++) {
            foreach ($directionIndex in 0,1) {
                $slot = $poseIndex * 2 + $directionIndex
                $col = $slot % $cols
                $row = [Math]::Floor($slot / $cols)
                $cellX = $col * $cellW
                $cellY = $row * $cellH
                $originX = $cellX + [Math]::Floor($cellW / 2)
                $originY = $cellY + 330
                $left = $directionIndex -eq 1
                $pose = $poses[$poseIndex]
                $frame = $frames[$pose.cf]
                $g.DrawLine($pen, $originX - 8, $originY, $originX + 8, $originY)
                $g.DrawLine($pen, $originX, $originY - 8, $originX, $originY + 8)
                $headOk = Draw-Part $g $head[$frame[0][0]] $frame[0] $left $originX $originY
                [void](Draw-Part $g $leg[$frame[1][0]] $frame[1] $left $originX $originY)
                [void](Draw-Part $g $body[$frame[2][0]] $frame[2] $left $originX $originY)
                $dirLabel = if ($left) { 'trai / transform 2' } else { 'phai / transform 0' }
                $g.DrawString("$($pose.label) - $dirLabel", $font, [System.Drawing.Brushes]::White, $cellX + 8, $cellY + 8)
                if (-not $headOk) {
                    $headIndex = $frame[0][0]
                    $headId = $head[$headIndex].id
                    $g.DrawString("THIEU DAU id $headId (slot $headIndex)", $smallFont, [System.Drawing.Brushes]::OrangeRed, $cellX + 8, $cellY + 29)
                }
                $g.DrawRectangle($pen, $cellX, $cellY, $cellW - 1, $cellH - 1)
            }
        }
    } finally {
        $font.Dispose(); $smallFont.Dispose(); $pen.Dispose()
    }
} finally { $g.Dispose() }

$outputDir = Split-Path -Parent $OutputPath
[System.IO.Directory]::CreateDirectory($outputDir) | Out-Null
$atlas.Save($OutputPath, [System.Drawing.Imaging.ImageFormat]::Png)
$atlas.Dispose()
Write-Output $OutputPath
