#!/usr/bin/env python3
import subprocess
import argparse
import os

chars = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~" # noqa
num_cols = 15
width_px = 34
ht_px = 72

parser = argparse.ArgumentParser()
parser.add_argument('source', type=str,
                    help='Source image to crop from')
parser.add_argument('dest', type=str,
                    help='Dest dir to dump into')
args = parser.parse_args()

x = 0
y = 0
idx = 0
for char in chars:
    if x >= num_cols:
        x = 0
        y += 1

    subprocess.call([
        'convert',
        args.source,
        '-crop', f'{width_px}x{ht_px}+{x*width_px}+{y*ht_px}',
        '-scale', '32x32',
        os.path.join(args.dest, f'{idx}.png')
        ])

    x += 1
    idx += 1
